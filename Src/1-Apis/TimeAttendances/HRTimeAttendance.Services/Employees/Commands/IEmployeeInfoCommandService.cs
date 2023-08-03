using HR.Common.DTOs.HumanResources.Employees;
using HR.Common.DTOs.HumanResources.Employees.EmployeeNames;
using HR.Common.Results;

namespace HRTimeAttendance.Services.Employees.Commands
{
    public interface IEmployeeInfoCommandService
    {
        ValueTask<ServiceResult> UpdateAsync<TEmployeeNames>(Guid employeeId, IEmployeeEntity<TEmployeeNames> employee)
            where TEmployeeNames : IEmployeeNameEntity;
    }
}
