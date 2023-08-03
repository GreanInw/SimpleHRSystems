using HR.Common.DTOs.HumanResources.Departments;
using HR.Common.DTOs.HumanResources.Departments.Responses;
using HR.Common.Results;

namespace HRTimeAttendance.Services.Departments.Queries
{
    public interface IDepartmentQueryService
    {
        ValueTask<ServiceResult<IEnumerable<T>>> GetAsync<T>(IGetDepartmentEntity entity) where T : DepartmentBaseResponse, new();
    }
}
