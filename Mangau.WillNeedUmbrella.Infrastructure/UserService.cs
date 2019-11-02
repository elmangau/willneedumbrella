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
using System.Threading.Tasks;

namespace Mangau.WillNeedUmbrella.Infrastructure
{
    public interface IUserService
    {
        public Task<UserDetails> Authenticate(string username, string password);

        public Task<IEnumerable<UserDetails>> GetAll();
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

        public async Task<UserDetails> Authenticate(string username, string password)
        {
            var bpassword = BCrypt.Net.BCrypt.HashPassword(password, 10);

            var user = await _wnuContext.Users
                .Where(u => u.UserName == username && u.Active)
                .Include(u => u.GroupsUsers)
                .ThenInclude(gu => gu.Group)
                .ThenInclude(g => g.GroupsPermissions)
                .ThenInclude(gp => gp.Permission)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }
            else if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            var claims = new List<Claim>() { new Claim(ClaimTypes.Name, user.Id.ToString()) };
            var res = new UserDetails(user, true);
            foreach(var p in res.Permissions)
            {
                claims.Add(new Claim(ClaimTypes.Role, p));
            }

            var tokenHnd = new JwtSecurityTokenHandler();
            var jwtKey = Encoding.ASCII.GetBytes(_appSettings.Authentication.JwtSecretKey);
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHnd.CreateToken(tokenDesc);
            res.Token = tokenHnd.WriteToken(token);

            return res;
        }

        public async Task<IEnumerable<UserDetails>> GetAll()
        {
            var users = await _wnuContext.Users.Where(u => u.Active).ToListAsync();

            return users.Select(u => new UserDetails(u));
        }
    }
}
