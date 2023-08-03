using HR.Common.Constants;
using HR.Common.DALs.Repositories.HumanResources.LeaveTables.Commands;
using HR.Common.DALs.UnitOfWorks;
using HR.Common.DTOs.HumanResources.LeaveTables;
using HR.Common.Libs.Extensions;
using HR.Common.Models.HumanResources;
using HR.Common.Results;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace HRTimeAttendance.Services.LeaveTables.Commands
{
    public class LeaveTableCommandService : HRCommonCommandService, ILeaveTableCommandService
    {
        public LeaveTableCommandService(IHttpContextAccessor httpContextAccessor, IHRUnitOfWork unitOfWork
            , ILeaveTableCommandRepository leaveTableCommandRepository)
            : base(httpContextAccessor, unitOfWork)
        {
            LeaveTableCommandRepository = leaveTableCommandRepository;
        }

        protected ILeaveTableCommandRepository LeaveTableCommandRepository { get; }

        public ValueTask<ServiceResult> CreateAsync(ICreateLeaveTableEntity entity)
            => CreateOrUpdateInternalAsync(entity);

        public ValueTask<ServiceResult> UpdateAsync(IUpdateLeaveTableEntity entity)
            => CreateOrUpdateInternalAsync(entity, entity);

        protected async ValueTask<ServiceResult> CreateOrUpdateInternalAsync(ICreateLeaveTableEntity createEntity
            , IUpdateLeaveTableEntity updateEntity = null)
        {
            var validateResult = await ValidateInternalAsync(createEntity, updateEntity);
            if (!validateResult.IsSuccess)
            {
                return new ServiceResult(validateResult.Errors);
            }

            bool isEdit = updateEntity is not null;
            var newOrUpdate = isEdit ? validateResult.Result
                : new LeaveTable
                {
                    Id = Guid.NewGuid(),
                    Name = createEntity.Name,
                    Days = createEntity.Days,
                    IsActive = true,
                    LanguageId = createEntity.LanguageId,
                };

            if (isEdit)
            {
                newOrUpdate.Name = updateEntity.Name;
                newOrUpdate.Days = updateEntity.Days;
                newOrUpdate.IsActive = updateEntity.IsActive;
                newOrUpdate.LanguageId = updateEntity.LanguageId;
                LeaveTableCommandRepository.Edit(newOrUpdate);
            }
            else
            {
                await LeaveTableCommandRepository.AddAsync(newOrUpdate);
            }

            await UnitOfWork.CommitAsync();
            return new ServiceResult(true);
        }

        protected async ValueTask<ServiceResult<LeaveTable>> ValidateInternalAsync(ICreateLeaveTableEntity createEntity
            , IUpdateLeaveTableEntity updateEntity = null)
        {
            var validateLanguage = await ValidateLanguageIdInternalAsync(createEntity.LanguageId);
            if (!validateLanguage.IsSuccess)
            {
                return new ServiceResult<LeaveTable>(validateLanguage.Errors);
            }

            LeaveTable entity = null;
            bool isEdit = updateEntity is not null;
            Expression<Func<LeaveTable, bool>> expression = w
                => w.Name == createEntity.Name && w.LanguageId == createEntity.LanguageId
                    && (!isEdit || w.Id != updateEntity.Id);

            if (await LeaveTableCommandRepository.AnyAsync(expression))
            {
                return new ServiceResult<LeaveTable>(
                    string.Format(ErrorMessageConstants.HumanResources.LeaveTables.NameDuplicate
                        , createEntity.Name));
            }

            if (isEdit)
            {
                entity = await LeaveTableCommandRepository.GetByIdAsync(updateEntity.Id);
                if (entity.IsNullable())
                {
                    return new ServiceResult<LeaveTable>(ErrorMessageConstants.HumanResources.LeaveTables.LeaveNotFound);
                }
            }

            return new ServiceResult<LeaveTable>(true, entity);
        }
    }
}