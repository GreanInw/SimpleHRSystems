using HR.Common.Constants;
using HR.Common.DALs.Repositories.Identities.Roles.Commands;
using HR.Common.DALs.UnitOfWorks;
using HR.Common.DTOs.Identities.Roles;
using HR.Common.Models.Identities;
using HR.Common.Results;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;

namespace Identities.Services.Roles.Commands
{
    public class RoleCommandService : BaseCommonCommandService<IHRUnitOfWork>, IRoleCommandService
    {
        public RoleCommandService(IHttpContextAccessor httpContextAccessor, IHRUnitOfWork unitOfWork
            , IRoleCommandRepository roleCommandRepository) : base(httpContextAccessor, unitOfWork)
        {
            RoleCommandRepository = roleCommandRepository;
        }

        protected IRoleCommandRepository RoleCommandRepository { get; }

        public async ValueTask<ServiceResult> CreateAsync(ICreateRoleEntity entity)
        {
            if (await RoleCommandRepository.AnyAsync(w => w.Name == entity.Name))
            {
                return new ServiceResult(string.Format(ErrorMessageConstants.Identities.Roles.NameDuplicate, entity.Name));
            }

            await RoleCommandRepository.AddAsync(new Role
            {
                Id = Guid.NewGuid(),
                Name = entity.Name,
                Description = entity.Description,
                IsActive = true
            });
            await UnitOfWork.CommitAsync();
            return new ServiceResult(true);
        }

        public async ValueTask<ServiceResult> UpdateAsync(IUpdateRoleEntity entity)
        {
            var role = await RoleCommandRepository.GetByIdAsync(entity.Id);
            if (role is null)
            {
                return new ServiceResult(ErrorMessageConstants.Identities.Roles.RoleNotFound);
            }

            role.Description = entity.Description;
            role.IsActive = entity.IsActive;

            RoleCommandRepository.Edit(role);
            await UnitOfWork.CommitAsync();
            return new ServiceResult(true);
        }
    }
}
