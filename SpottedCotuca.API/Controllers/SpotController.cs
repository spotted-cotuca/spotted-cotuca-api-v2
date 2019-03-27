using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpottedCotuca.API.Utils;
using SpottedCotuca.Application.Contracts.Requests;
using SpottedCotuca.Application.Contracts.Requests.Spot;
using SpottedCotuca.Application.Services;

namespace SpottedCotuca.API.Controllers
{
    [Route("api/v1/spots")]
    [ApiController]
    public class SpotController : ControllerBase
    {
        private readonly SpotService _service;

        public SpotController(SpotService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _service.ReadSpot(id);

            return HttpHelper.Convert(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaging([FromQuery] SpotsGetRequest request)
        {
            var result = await _service.ReadSpots(request);

            return HttpHelper.Convert(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(SpotPostRequest request)
        {
            var result = await _service.CreateSpot(request);

            return HttpHelper.Convert(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, SpotPutRequest request)
        {
            var result = await _service.UpdateSpot(id, request);

            return HttpHelper.Convert(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteSpot(id);

            return HttpHelper.Convert(result);
        }
    }
}
