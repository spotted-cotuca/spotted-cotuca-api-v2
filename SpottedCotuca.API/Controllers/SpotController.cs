using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpottedCotuca.Application.Contracts.Requests;
using SpottedCotuca.Application.Services;
using SpottedCotuca.Application.Utils;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var spot = await _service.ReadSpot(id);
            return Ok(spot);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaging([FromQuery] GetPagingSpotsRequest request)
        {
            var pagingSpots = await _service.ReadPagingSpots(request.Status.ToStatus(), request.Offset, request.Limit);
            return Ok(pagingSpots);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostSpotRequest request)
        {
            await _service.CreateSpot(request.Message);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PutSpotRequest request)
        {
            await _service.UpdateSpot(id, request.Status.ToStatus());
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteSpot(id);
            return Ok();
        }
    }
}
