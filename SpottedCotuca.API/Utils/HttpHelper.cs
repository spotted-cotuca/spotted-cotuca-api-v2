using Microsoft.AspNetCore.Mvc;
using SpottedCotuca.Application.Services.Definitions;
using System;
using System.Net;

namespace SpottedCotuca.API.Utils
{
    public class HttpHelper
    {
        public static IActionResult Convert(Result result)
        {
            if (result == null)
            {
                throw new ArgumentException("Result cannot be null.", "result");
            }

            if (result.Success)
            {
                return new OkResult();
            }

            if (result.MetaError.StatusCode == (int)HttpStatusCode.NotFound)
            {
                return new NotFoundObjectResult(result.MetaError.Error);
            }

            if (result.MetaError.StatusCode == (int)HttpStatusCode.BadRequest)
            {
                return new BadRequestObjectResult(result.MetaError.Error);
            }

            return new ObjectResult(result.MetaError.Error)
            {
                StatusCode = (int)result.MetaError.StatusCode
            };
        }

        public static IActionResult Convert<T>(Result<T> result)
        {
            if (result == null)
            {
                throw new ArgumentException("Result cannot be null.", "result");
            }

            if (result.Success)
            {
                return new OkObjectResult(result.Obj);
            }

            if (result.MetaError.StatusCode == (int)HttpStatusCode.NotFound)
            {
                return new NotFoundObjectResult(result.MetaError.Error);
            }

            if (result.MetaError.StatusCode == (int)HttpStatusCode.BadRequest)
            {
                return new BadRequestObjectResult(result.MetaError.Error);
            }

            return new ObjectResult(result.MetaError.Error)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
