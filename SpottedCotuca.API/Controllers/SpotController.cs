using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpottedCotuca.API.Models;
using SpottedCotuca.API.Services;
using SpottedCotuca.API.Utils;

namespace SpottedCotuca.API.Controllers
{
    [Route("api/v1/spots")]
    [ApiController]
    public class SpotController : ControllerBase
    {
        private readonly ISpotService _service;

        public SpotController(ISpotService service)
        {
            _service = service;
        }

        [HttpGet("{:id}")]
        public async Task<IActionResult> Get(long id)
        {
            var spot = await _service.ReadSpot(id);
            return Ok(spot);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaging(string status, [FromQuery(Name = "offset")] int offset = 0, [FromQuery(Name = "limit")] int limit = 50)
        {
            var pagingSpots = await _service.ReadPagingSpots(status.ToStatus(), offset, limit);
            return Ok(pagingSpots);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string message)
        {
            await _service.CreateSpot(message);
            return Ok();
        }

        [HttpPut("{:id}")]
        public async Task<IActionResult> Put(long id, Status status)
        {
            await _service.UpdateSpot(id, status);
            return Ok();
        }

        [HttpDelete("{:id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteSpot(id);
            return Ok();
        }
    }
}
