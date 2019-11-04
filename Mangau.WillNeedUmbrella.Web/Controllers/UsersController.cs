using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mangau.WillNeedUmbrella.Infrastructure;
using Mangau.WillNeedUmbrella.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mangau.WillNeedUmbrella.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserAuthentication ua, CancellationToken cancellationToken)
        {
            var user = await _userService.Login(ua.Username, ua.Password, cancellationToken);

            if (user == null)
            {
                logger.Error($"Username '{ua.Username}' or password is incorrect");
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(user);
        }

        [HttpDelete("logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            if (Int64.TryParse(User.Identity.Name, out long sessionTokenId))
            {
                await _userService.Logout(sessionTokenId, cancellationToken);

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _userService.GetAll(cancellationToken));
        }
    }
}
