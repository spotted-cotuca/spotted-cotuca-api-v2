using System.Linq;
using System.Threading.Tasks;
using SpottedCotuca.Aplication.Repositories;
using SpottedCotuca.Application.Contracts.Requests.Role;
using SpottedCotuca.Application.Contracts.Responses.Role;
using SpottedCotuca.Application.Contracts.Validator;
using SpottedCotuca.Application.Entities.Models;
using SpottedCotuca.Application.Services.Definitions;
using SpottedCotuca.Application.Services.Utils;
using SpottedCotuca.Application.Utils;

namespace SpottedCotuca.Application.Services
{
    public class RoleService : BaseService
    {
        private readonly RoleRepository _repository;

        public RoleService(RoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<RoleGetResponse>> ReadRole(string name)
        {
            Role role;

            try { role = await _repository.Read(name); }
            catch { return Error<RoleGetResponse>(Errors.RoleReadingFromDatabaseError); }

            return role == null ? Error<RoleGetResponse>(Errors.RoleNotFoundError) : Success(role.ToRoleGetResponse());
        }

        public async Task<Result<RolePostResponse>> CreateRole(RolePostRequest request)
        {
            var validate = await request.ValidateAsync();
            if (!validate.IsValid)
            {
                var error = validate.Errors.FirstOrDefault();
                return Error<RolePostResponse>(error.ToMetaError());
            }

            var role = new Role
            {
                Name = request.Name,
                Permissions = request.Permissions.ToPermissionList()
            };

            try { role = await _repository.Create(role); }
            catch { return Error<RolePostResponse>(Errors.RoleCreatingOnDatabaseError); }

            return Success(role.ToRolePostResponse());
        }

        public async Task<Result<RolePutResponse>> UpdateRole(string name, RolePutRequest request)
        {
            var validate = await request.ValidateAsync();
            if (!validate.IsValid)
            {
                var error = validate.Errors.FirstOrDefault();
                return Error<RolePutResponse>(error.ToMetaError());
            }

            Role role;
            try { role = await _repository.Read(name); }
            catch { return Error<RolePutResponse>(Errors.RoleReadingFromDatabaseError); }

            if (role == null)
                return Error<RolePutResponse>(Errors.RoleNotFoundError);

            role.Permissions = request.Permissions.ToPermissionList();
            try { role = await _repository.Update(role); }
            catch { return Error<RolePutResponse>(Errors.RoleUpdatingOnDatabaseError); }

            return Success(role.ToRolePutResponse());
        }

        public async Task<Result> DeleteRole(string name)
        {
            Role role;
            try { role = await _repository.Read(name); }
            catch { return Error(Errors.RoleReadingFromDatabaseError);  }

            if (role == null)
                return Error(Errors.RoleNotFoundError);

            try { await _repository.Delete(name); }
            catch { return Error(Errors.RoleReadingFromDatabaseError);  }

            return Success();
        }
    }
}
