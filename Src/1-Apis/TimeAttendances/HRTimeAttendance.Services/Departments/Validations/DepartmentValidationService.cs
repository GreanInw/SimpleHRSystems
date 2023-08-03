using HR.Common.Constants;
using HR.Common.DALs.Repositories.HumanResources.Departments.Queries;
using HR.Common.Results;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;

namespace HRTimeAttendance.Services.Departments.Validations
{
    public class DepartmentValidationService : BaseCommonService, IDepartmentValidationService
    {
        private readonly IDepartmentQueryRepository _departmentQueryRepository;

        public DepartmentValidationService(IHttpContextAccessor httpContextAccessor
            , IDepartmentQueryRepository departmentQueryRepository) : base(httpContextAccessor)
        {
            _departmentQueryRepository = departmentQueryRepository;
        }

        public async ValueTask<ServiceResult> ValidateIdAsync(Guid id)
            => (await _departmentQueryRepository.AnyAsync(w => w.Id == id)) ? new ServiceResult(true)
                : new ServiceResult(ErrorMessageConstants.HumanResources.Departments.DepartmentNotFound);
    }
}