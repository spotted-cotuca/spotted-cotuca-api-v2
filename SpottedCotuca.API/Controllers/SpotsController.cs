using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpottedCotuca.API.Models;
using SpottedCotuca.API.Responses;
using SpottedCotuca.API.Services;

namespace SpottedCotuca.API.Controllers
{
    [Route("api/v1/spots")]
    [ApiController]
    public class SpotsController : ControllerBase
    {
        private readonly ISpotsService _spotsService;

        public SpotsController(ISpotsService spotsService)
        {
            _spotsService = spotsService;
        }

        [HttpGet("{:id}")]
        public ActionResult<GetSpotResponse> Get(long id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult<GetSpotsResponse> GetPaging([FromQuery(Name = "offset")] int offset = 0, [FromQuery(Name = "limit")] int limit = 50)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Post(string message)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{:id}")]
        public ActionResult Put(long id, Status status)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{:id}")]
        public ActionResult Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
