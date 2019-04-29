using System;
using System.Collections.Generic;
using FluentValidation;
using SpottedCotuca.Application.Contracts.Requests.Spot;
using SpottedCotuca.Application.Contracts.Validator;
using SpottedCotuca.Application.Services;
using SpottedCotuca.Common.Security;

namespace SpottedCotuca.Application.Contracts.Requests.Role
{
    public class RolePostRequest
    {
        public string Name { get; set; }
        public List<string> Permissions { get; set; }
    }

    public class RolePostRequestValidator : AbstractValidator<RolePostRequest>
    {
        public RolePostRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithCustomError(Errors.RoleNameIsEmpty)
                .Length(0, 30)
                .WithCustomError(Errors.RoleNameIsMoreThan30Characters);
            
            RuleFor(request => request.Permissions)
                .NotEmpty()
                .WithCustomError(Errors.RolePermissionsIsEmpty)
                .Must(BeAValidPermissionArray)
                .WithCustomError(Errors.RolePermissionsArrayIsInvalid);
        }
        
        private bool BeAValidPermissionArray(List<string> permissions)
        {
            return permissions.TrueForAll(
                permission => Enum.TryParse(typeof(Permission), permission, true, out var p)
            );
        }
    }
}
