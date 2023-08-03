using HR.Common.Constants;
using HR.Common.DALs.Repositories.HumanResources.Departments.Commands;
using HR.Common.DALs.UnitOfWorks;
using HR.Common.DTOs.HumanResources.Departments;
using HR.Common.Models.HumanResources;
using HR.Common.Results;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace HRTimeAttendance.Services.Departments.Commands
{
    public class DepartmentCommandService : HRCommonCommandService, IDepartmentCommandService
    {
        public DepartmentCommandService(IHttpContextAccessor httpContextAccessor, IHRUnitOfWork unitOfWork
            , IDepartmentCommandRepository departmentCommandRepository)
            : base(httpContextAccessor, unitOfWork)
        {
            DepartmentCommandRepository = departmentCommandRepository;
        }

        protected IDepartmentCommandRepository DepartmentCommandRepository { get; }

        public async ValueTask<ServiceResult> CreateAsync(ICreateDepartmentEntity entity)
            => await CreateOrUpdateAsync(entity);

        public async ValueTask<ServiceResult> UpdateAsync(IUpdateDepartmentEntity entity)
            => await CreateOrUpdateAsync(entity, entity);

        protected async ValueTask<ServiceResult> CreateOrUpdateAsync(ICreateDepartmentEntity createEntity
            , IUpdateDepartmentEntity updateEntity = null)
        {
            var validate = await ValidateCreateOrUpdateInternalAsync(createEntity, updateEntity);
            if (!validate.IsSuccess)
            {
                return validate;
            }

            bool isEdit = updateEntity is not null;
            var createOrUpdateEntity = isEdit
                ? await DepartmentCommandRepository.GetByIdAsync(updateEntity.Id)
                : new Department
                {
                    Id = Guid.NewGuid(),
                    Name = createEntity.Name,
                    LanguageId = createEntity.LanguageId,
                    ParentId = createEntity.ParentId,
                    IsActive = true,
                };

            if (isEdit)
            {
                createOrUpdateEntity.Name = updateEntity.Name;
                createOrUpdateEntity.ParentId = updateEntity.ParentId;
                createOrUpdateEntity.IsActive = updateEntity.IsActive;
                createOrUpdateEntity.LanguageId = updateEntity.LanguageId;
                DepartmentCommandRepository.Edit(createOrUpdateEntity);
            }
            else
            {
                await DepartmentCommandRepository.AddAsync(createOrUpdateEntity);
            }

            await UnitOfWork.CommitAsync();
            return new ServiceResult(true);
        }

        protected internal async ValueTask<ServiceResult> ValidateCreateOrUpdateInternalAsync(ICreateDepartmentEntity createEntity
            , IUpdateDepartmentEntity updateEntity = null)
        {
            bool isEdit = updateEntity is not null;
            var validateLanguage = await ValidateLanguageIdInternalAsync(createEntity.LanguageId);
            if (!validateLanguage.IsSuccess)
            {
                return new ServiceResult(validateLanguage.Errors);
            }

            Expression<Func<Department, bool>> expression = w => w.Name == createEntity.Name
                && w.ParentId == createEntity.ParentId && w.LanguageId == createEntity.LanguageId
                && (!isEdit || w.Id != updateEntity.Id);

            if (await DepartmentCommandRepository.AnyAsync(expression))
            {
                return new ServiceResult(string.Format(
                    ErrorMessageConstants.HumanResources.Departments.DepartmentDuplicate, createEntity.Name));
            }

            if (isEdit)
            {
                if (updateEntity.ParentId.HasValue
                    && !await DepartmentCommandRepository.AnyAsync(w => w.Id == updateEntity.ParentId.Value))
                {
                    return new ServiceResult(ErrorMessageConstants.HumanResources.Departments.ParentIdNotFound);
                }

                if (!await DepartmentCommandRepository.AnyAsync(w => w.Id == updateEntity.Id))
                {
                    return new ServiceResult(ErrorMessageConstants.HumanResources.Departments.DepartmentNotFound);
                }
            }

            return new ServiceResult(true);
        }
    }
}
