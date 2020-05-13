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
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? page, [FromQuery] int? size, CancellationToken cancellationToken)
        {
            return Ok(await _cityService.GetAll(new PageRequest(page, size), cancellationToken));
        }

        [HttpGet("country/{country}")]
        public async Task<IActionResult> GetAllByCountry(string country, [FromQuery] int? page, [FromQuery] int? size, CancellationToken cancellationToken)
        {
            return Ok(await _cityService.GetAllByCountry(new PageRequest(page, size), country, cancellationToken));
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetAllByName(string name, [FromQuery] int? page, [FromQuery] int? size, CancellationToken cancellationToken)
        {
            return Ok(await _cityService.GetAllByName(new PageRequest(page, size), name, cancellationToken));
        }

        [HttpGet("country/{country}/name/{name}")]
        public async Task<IActionResult> GetAllByCountryAndName(string country, string name, [FromQuery] int? page, [FromQuery] int? size, CancellationToken cancellationToken)
        {
            return Ok(await _cityService.GetAllByCountryAndName(new PageRequest(page, size), country, name, cancellationToken));
        }
    }
}
