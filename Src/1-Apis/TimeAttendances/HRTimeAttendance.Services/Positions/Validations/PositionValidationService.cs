using HR.Common.Constants;
using HR.Common.DALs.Repositories.HumanResources.Positions.Commands;
using HR.Common.Results;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;

namespace HRTimeAttendance.Services.Positions.Validations
{
    public class PositionValidationService : BaseCommonService, IPositionValidationService
    {
        private readonly IPositionCommandRepository _positionCommandRepository;

        public PositionValidationService(IHttpContextAccessor httpContextAccessor
            , IPositionCommandRepository positionCommandRepository) : base(httpContextAccessor)
        {
            _positionCommandRepository = positionCommandRepository;
        }

        public async ValueTask<ServiceResult> ValidateIdAsync(Guid id)
            => await _positionCommandRepository.AnyAsync(w => w.Id == id) ? new ServiceResult(true)
                : new ServiceResult(ErrorMessageConstants.HumanResources.Departments.DepartmentNotFound);
    }
}