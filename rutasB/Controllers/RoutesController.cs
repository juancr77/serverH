using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelApi.Models;
using TravelApi.Services;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly OsrmService _osrmService;

        public RoutesController(OsrmService osrmService)
        {
            _osrmService = osrmService;
        }

        [HttpPost("GetRoute")]
        public async Task<IActionResult> GetRoute([FromBody] RouteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Start) || string.IsNullOrWhiteSpace(request.End))
                return BadRequest("Start and End locations are required.");

            var route = await _osrmService.GetRouteAsync(request.Start, request.End);

            if (route == null || route.Distance == 0)
                return NotFound("Route not found.");

            return Ok(route);
        }
    }
}
