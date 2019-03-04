using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SpottedCotuca.Application.Services;
using SpottedCotuca.Application.Contracts.Requests;
using SpottedCotuca.Application.Entities.Models;

namespace SpottedCotuca.API.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("{:username}")]
        public async Task<IActionResult> Get(string username)
        {
            User user = await _service.ReadUser(username);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserPostRequest signupRequest)
        {
            await _service.Signup(signupRequest.ToUser());
            return Ok();
        }

        [HttpDelete("{:username}")]
        public async Task<IActionResult> Delete(string username)
        {
            await _service.DeleteUser(username);
            return Ok();
        }
    }
}
