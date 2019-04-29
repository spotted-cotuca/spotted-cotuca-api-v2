using System.Threading.Tasks;
using FluentValidation.Results;
using SpottedCotuca.Application.Contracts.Requests.Role;
using SpottedCotuca.Application.Contracts.Requests.Spot;
using SpottedCotuca.Application.Contracts.Requests.User;

namespace SpottedCotuca.Application.Contracts.Validator
{
    public static class RoleValidatorExtensions
    {
        public static async Task<ValidationResult> ValidateAsync(this RolePutRequest request)
        {
            var validator = new RolePutRequestValidator();
            return await validator.ValidateAsync(request);
        }

        public static async Task<ValidationResult> ValidateAsync(this RolePostRequest request)
        {
            var validator = new RolePostRequestValidator();
            return await validator.ValidateAsync(request);
        }
    }
}
