using System.Threading.Tasks;
using FluentValidation.Results;
using SpottedCotuca.Application.Contracts.Requests.Spot;
using SpottedCotuca.Application.Contracts.Requests.User;

namespace SpottedCotuca.Application.Contracts.Validator
{
    public static class UserValidatorExtensions
    {
        public static async Task<ValidationResult> ValidateAsync(this UserPostRequest request)
        {
            var validator = new UserPostRequestValidator();
            return await validator.ValidateAsync(request);
        }
    }
}
