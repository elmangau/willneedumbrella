using Mangau.WillNeedUmbrella.Configuration;
using Mangau.WillNeedUmbrella.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mangau.WillNeedUmbrella.Infrastructure
{
    public interface IUserService
    {
        public Task<UserDetails> Login(string username, string password, CancellationToken cancellationToken = default);

        public Task<bool> Logout(long userId, CancellationToken cancellationToken = default);

        public Task<IEnumerable<UserDetails>> GetAll(CancellationToken cancellationToken = default);

        public Task<IEnumerable<User>> GetLoggedIn(CancellationToken cancellationToken);

        public Task<bool> LogoutExpired(CancellationToken cancellationToken);
    }

    public class UserService : IUserService
    {
        private AppSettings _appSettings;
        private WnuContextBase _wnuContext;

        public UserService(AppSettings appSettings, WnuContextBase wnuContext)
        {
            _appSettings = appSettings;
            _wnuContext = wnuContext;
        }

        public async Task<UserDetails> Login(string username, string password, CancellationToken cancellationToken = default)
        {
            var bpassword = BCrypt.Net.BCrypt.HashPassword(password, 10);

            var user = await _wnuContext.Users
                .Include(u => u.GroupsUsers)
                .ThenInclude(gu => gu.Group)
                .ThenInclude(g => g.GroupsPermissions)
                .ThenInclude(gp => gp.Permission)
                .Where(u => u.UserName == username && u.Active)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                return null;
            }
            else if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            var expires = DateTime.UtcNow.AddDays(7);

            var st = await _wnuContext.SessionTokens.AddAsync(new SessionToken { UserId = user.Id, LoggedAt = DateTime.UtcNow, Expires = expires }, cancellationToken);
            await _wnuContext.SaveChangesAsync(cancellationToken);

            var res = new UserDetails(user, true);
            var claims = new List<Claim>() 
            {
                new Claim(ClaimTypes.Name, st.Entity.Id.ToString()),
                new Claim(ClaimTypes.Surname, user.Id.ToString()),
            };
            claims.AddRange(res.Permissions.Select(p => new Claim(ClaimTypes.Role, p)));

            var tokenHnd = new JwtSecurityTokenHandler();
            var jwtKey = Encoding.ASCII.GetBytes(_appSettings.Authentication.JwtSecretKey);
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHnd.CreateToken(tokenDesc);
            res.Token = tokenHnd.WriteToken(token);

            return res;
        }

        public async Task<bool> Logout(long sessionTokenId, CancellationToken cancellationToken = default)
        {
            _wnuContext.SessionTokens.RemoveRange(await _wnuContext.SessionTokens.Where(st => st.Id == sessionTokenId).ToArrayAsync());
            await _wnuContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IEnumerable<UserDetails>> GetAll(CancellationToken cancellationToken = default)
        {
            var users = await _wnuContext.Users.Where(u => u.Active).ToListAsync(cancellationToken);

            return users.Select(u => new UserDetails(u));
        }

        public async Task<IEnumerable<User>> GetLoggedIn(CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var res = await _wnuContext.SessionTokens
                .Include(st => st.User)
                .Where(st => st.UserId > 0 && st.User.Active && st.Expires > now)
                .Select(st => st.User)
                .ToListAsync(cancellationToken);

            return res;
        }

        public async Task<bool> LogoutExpired(CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var res = await _wnuContext.SessionTokens
                .Where(st => st.Expires <= now)
                .ToListAsync(cancellationToken);

            if (res.Any())
            {
                _wnuContext.SessionTokens.RemoveRange(res);
                await _wnuContext.SaveChangesAsync(cancellationToken);
            }

            return true;
        }
    }
}
