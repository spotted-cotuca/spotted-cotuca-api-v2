using System;
using System.Collections.Generic;
using FluentValidation;
using SpottedCotuca.Application.Contracts.Validator;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Application.Services;
using SpottedCotuca.Common.Security;

namespace SpottedCotuca.Application.Contracts.Requests.Role
{
    public class RolePutRequest
    {
        public List<string> Permissions { get; set; }
    }

    public class RolePutRequestValidator : AbstractValidator<RolePutRequest>
    {
        public RolePutRequestValidator()
        {
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
