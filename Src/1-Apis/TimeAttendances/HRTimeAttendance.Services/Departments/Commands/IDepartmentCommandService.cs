using HR.Common.DTOs.HumanResources.Departments;
using HR.Common.Results;

namespace HRTimeAttendance.Services.Departments.Commands
{
    public interface IDepartmentCommandService
    {
        ValueTask<ServiceResult> CreateAsync(ICreateDepartmentEntity entity);
        ValueTask<ServiceResult> UpdateAsync(IUpdateDepartmentEntity entity);
    }
}
