using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpottedCotuca.API.Utils;
using SpottedCotuca.Application.Contracts.Requests;
using SpottedCotuca.Application.Contracts.Requests.Role;
using SpottedCotuca.Application.Contracts.Requests.Spot;
using SpottedCotuca.Application.Services;

namespace SpottedCotuca.API.Controllers
{
    [Route("api/v1/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _service;

        public RoleController(RoleService service)
        {
            _service = service;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var result = await _service.ReadRole(name);

            return HttpHelper.Convert(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(RolePostRequest request)
        {
            var result = await _service.CreateRole(request);

            return HttpHelper.Convert(result);
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> Put(string name, RolePutRequest request)
        {
            var result = await _service.UpdateRole(name, request);

            return HttpHelper.Convert(result);
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var result = await _service.DeleteRole(name);

            return HttpHelper.Convert(result);
        }
    }
}
