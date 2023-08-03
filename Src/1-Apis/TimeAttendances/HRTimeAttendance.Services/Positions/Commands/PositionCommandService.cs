using HR.Common.Constants;
using HR.Common.DALs.Repositories.HumanResources.Positions.Commands;
using HR.Common.DALs.UnitOfWorks;
using HR.Common.DTOs.HumanResources.Positions;
using HR.Common.Models.HumanResources;
using HR.Common.Results;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace HRTimeAttendance.Services.Positions.Commands
{
    public class PositionCommandService : HRCommonCommandService, IPositionCommandService
    {
        public PositionCommandService(IHttpContextAccessor httpContextAccessor, IHRUnitOfWork unitOfWork
            , IPositionCommandRepository positionCommandRepository)
            : base(httpContextAccessor, unitOfWork)
        {
            PositionCommandRepository = positionCommandRepository;
        }

        protected IPositionCommandRepository PositionCommandRepository { get; }

        public async ValueTask<ServiceResult> CreateAsync(ICreatePositionEntity entity)
            => await CreateOrUpdateInternalAsync(entity);

        public async ValueTask<ServiceResult> UpdateAsync(IUpdatePositionEntity entity)
            => await CreateOrUpdateInternalAsync(entity, entity);

        protected internal async ValueTask<ServiceResult> CreateOrUpdateInternalAsync(ICreatePositionEntity createEntity
            , IUpdatePositionEntity updateEntity = null)
        {
            var validate = await ValidateCreateOrUpdateInternalAsync(createEntity, updateEntity);
            if (!validate.IsSuccess)
            {
                return validate;
            }

            var isEdit = updateEntity is not null;
            var createOrUpdateEntity = isEdit
                ? await PositionCommandRepository.GetByIdAsync(updateEntity.Id)
                : new Position
                {
                    Id = Guid.NewGuid(),
                    Name = createEntity.Name,
                    LanguageId = createEntity.LanguageId,
                    IsActive = true
                };

            if (isEdit)
            {
                createOrUpdateEntity.Name = updateEntity.Name;
                createOrUpdateEntity.IsActive = updateEntity.IsActive;
                createOrUpdateEntity.LanguageId = updateEntity.LanguageId;
                PositionCommandRepository.Edit(createOrUpdateEntity);
            }
            else
            {
                await PositionCommandRepository.AddAsync(createOrUpdateEntity);
            }
            await UnitOfWork.CommitAsync();
            return new ServiceResult(true);
        }

        protected internal async ValueTask<ServiceResult> ValidateCreateOrUpdateInternalAsync(ICreatePositionEntity createEntity
            , IUpdatePositionEntity updateEntity = null)
        {
            bool isEdit = updateEntity is not null;
            Expression<Func<Position, bool>> expression = w => w.Name == createEntity.Name
                && w.LanguageId == createEntity.LanguageId
                && (!isEdit || w.Id != updateEntity.Id);

            if (await PositionCommandRepository.AnyAsync(expression))
            {
                return new ServiceResult(string.Format(
                    ErrorMessageConstants.HumanResources.Positions.PositionDuplicate, createEntity.Name));
            }

            if (isEdit && !await PositionCommandRepository.AnyAsync(w => w.Id == updateEntity.Id))
            {
                return new ServiceResult(ErrorMessageConstants.HumanResources.Positions.PositionNotFound);
            }

            return new ServiceResult(true);
        }
    }
}