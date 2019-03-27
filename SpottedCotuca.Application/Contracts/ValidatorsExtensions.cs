using FluentValidation.Results;
using SpottedCotuca.Application.Contracts.Requests;
using System.Threading.Tasks;
using SpottedCotuca.Application.Contracts.Requests.Spot;
using SpottedCotuca.Application.Contracts.Requests.User;

namespace SpottedCotuca.Application.Contracts
{
    public static class ValidatorsExtensions
    {
        public async static Task<ValidationResult> ValidateAsync(this SpotPostRequest request)
        {
            var validator = new SpotPostRequestValidator();
            return await validator.ValidateAsync(request);
        }

        public async static Task<ValidationResult> ValidateAsync(this SpotPutRequest request)
        {
            var validator = new SpotPutRequestValidator();
            return await validator.ValidateAsync(request);
        }

        public async static Task<ValidationResult> ValidateAsync(this SpotsGetRequest request)
        {
            var validator = new SpotsGetRequestValidator();
            return await validator.ValidateAsync(request);
        }

        public async static Task<ValidationResult> ValidateAsync(this UserPostRequest request)
        {
            var validator = new UserPostRequestValidator();
            return await validator.ValidateAsync(request);
        }
    }
}
