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
        private readonly ILogger<UsersController> _logger;
        private IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthentication model, CancellationToken cancellationToken)
        {
            var user = await _userService.Authenticate(model.Username, model.Password, cancellationToken);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(user);
        }

        [HttpDelete]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            if (Int64.TryParse(User.Identity.Name, out long userId))
            {
                await _userService.Logout(userId, cancellationToken);

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
