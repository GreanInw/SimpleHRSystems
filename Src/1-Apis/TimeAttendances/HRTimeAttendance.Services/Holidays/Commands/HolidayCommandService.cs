using HR.Common.Constants;
using HR.Common.DALs.Repositories.HumanResources.Holidays.Commands;
using HR.Common.DALs.UnitOfWorks;
using HR.Common.DTOs.HumanResources.Holidays;
using HR.Common.Libs.Extensions;
using HR.Common.Models.HumanResources;
using HR.Common.Results;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace HRTimeAttendance.Services.Holidays.Commands
{
    public class HolidayCommandService : HRCommonCommandService, IHolidayCommandService
    {
        public HolidayCommandService(IHttpContextAccessor httpContextAccessor, IHRUnitOfWork unitOfWork
            , IHolidayCommandRepository holidayCommandRepository)
            : base(httpContextAccessor, unitOfWork)
        {
            HolidayCommandRepository = holidayCommandRepository;
        }

        protected IHolidayCommandRepository HolidayCommandRepository { get; }

        public async ValueTask<ServiceResult> CreateAsync(ICreateHolidayEntity entity)
            => await CreateOrUpdateInternalAsync(entity);

        public async ValueTask<ServiceResult> UpdateAsync(IUpdateHolidayEntity entity)
            => await CreateOrUpdateInternalAsync(entity, entity);

        protected async ValueTask<ServiceResult> CreateOrUpdateInternalAsync(ICreateHolidayEntity createEntity
            , IUpdateHolidayEntity updateEntity = null)
        {
            var validate = await ValidateCreateOrUpdateInternalAsync(createEntity, updateEntity);
            if (!validate.IsSuccess)
            {
                return validate;
            }

            bool isNew = updateEntity is null;
            var createOrUpdateEntity = isNew
                ? new Holiday
                {
                    Id = Guid.NewGuid(),
                    LanguageId = createEntity.LanguageId,
                    Date = createEntity.Date.ToRawUSDateTimeFromRequest(),
                    Name = createEntity.Name,
                    IsActive = true
                } : await HolidayCommandRepository.GetByIdAsync(updateEntity.Id);

            if (isNew)
            {
                await HolidayCommandRepository.AddAsync(createOrUpdateEntity);
            }
            else
            {
                createOrUpdateEntity.Date = updateEntity.Date.ToRawUSDateTimeFromRequest();
                createOrUpdateEntity.Name = updateEntity.Name;
                createOrUpdateEntity.LanguageId = createEntity.LanguageId;
                createOrUpdateEntity.IsActive = updateEntity.IsActive;
                HolidayCommandRepository.Edit(createOrUpdateEntity);
            }

            await UnitOfWork.CommitAsync();
            return new ServiceResult(true);
        }

        protected async ValueTask<ServiceResult> ValidateCreateOrUpdateInternalAsync(ICreateHolidayEntity createEntity
            , IUpdateHolidayEntity updateEntity = null)
        {
            var validateLanguage = await ValidateLanguageIdInternalAsync(createEntity.LanguageId);
            if (!validateLanguage.IsSuccess)
            {
                return new ServiceResult(validateLanguage.Errors);
            }

            bool isEdit = updateEntity is not null;
            var holidayDate = createEntity.Date.ToRawUSDateTimeFromRequest();

            Expression<Func<Holiday, bool>> expression = w
                => w.LanguageId == createEntity.LanguageId && w.Date == holidayDate
                    && (!isEdit || w.Id != updateEntity.Id);

            if (await HolidayCommandRepository.AnyAsync(expression))
            {
                return new ServiceResult(string.Format(
                    ErrorMessageConstants.HumanResources.Holidays.DateDuplicate
                        , holidayDate.ToUSDateString()));
            }

            if (isEdit && !await HolidayCommandRepository.AnyAsync(w => w.Id == updateEntity.Id))
            {
                return new ServiceResult(ErrorMessageConstants.HumanResources.Holidays.HolidayNotFound);
            }

            return new ServiceResult(true);
        }
    }
}