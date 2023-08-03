using HR.Common.Constants;
using HR.Common.DALs.Repositories.Identities.Roles.Commands;
using HR.Common.DALs.Repositories.Identities.SSO.Commands;
using HR.Common.DALs.UnitOfWorks;
using HR.Common.DTOs.Identities.Users;
using HR.Common.Models.Identities;
using HR.Common.Results;
using Identities.Services.Users.Bases;
using Microsoft.AspNetCore.Http;

namespace Identities.Services.Users.Commands
{
    public class UserCommandService : UserCommandBaseService, IUserCommandService
    {
        public UserCommandService(IHttpContextAccessor httpContextAccessor, IHRUnitOfWork unitOfWork
            , IUserCommandRepository userCommandRepository
            , IRoleCommandRepository roleCommandRepository)
            : base(httpContextAccessor, unitOfWork, userCommandRepository)
        {
            RoleCommandRepository = roleCommandRepository;
        }

        protected IRoleCommandRepository RoleCommandRepository { get; }

        public async ValueTask<ServiceResult> ActiveOrInactiveAsync(string username, bool isActive)
        {
            var user = await GetUserAsync(username);
            if (user is null)
            {
                return new ServiceResult(ErrorMessageConstants.Identities.Users.UserNotFound);
            }

            user.IsActive = isActive;
            UserCommandRepository.Edit(user);
            await UnitOfWork.CommitAsync();
            return new ServiceResult(true);
        }

        public async ValueTask<ServiceResult> AssignRolesAsync(IAssignRolesToUserEntity entity)
        {
            var user = await GetUserAsync(entity.UserId, true, true);
            if (user is null)
            {
                return new ServiceResult(ErrorMessageConstants.Identities.Users.UserNotFound);
            }

            if (!await RoleCommandRepository.AnyAsync(w => entity.RoleIds.Contains(w.Id)))
            {
                return new ServiceResult(ErrorMessageConstants.Identities.Roles.RoleNotFound);
            }

            if (user.UserInRoles is null)
            {
                user.UserInRoles = new List<UserInRole>();
            }

            foreach (var item in entity.RoleIds)
            {
                if (user.UserInRoles.Any(w => w.RoleId == item))
                {
                    continue;
                }

                user.UserInRoles.Add(new UserInRole { UserId = entity.UserId, RoleId = item });
            }

            UserCommandRepository.Edit(user);
            await UnitOfWork.CommitAsync();

            return new ServiceResult(true);
        }
    }
}