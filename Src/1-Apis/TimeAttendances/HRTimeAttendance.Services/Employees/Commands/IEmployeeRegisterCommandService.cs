using HR.Common.DTOs.HumanResources.Employees.EmployeeNames;
using HR.Common.DTOs.HumanResources.Employees;
using HR.Common.DTOs.Identities.Registers;
using HR.Common.Results;

namespace HRTimeAttendance.Services.Employees.Commands
{
    public interface IEmployeeRegisterCommandService
    {
        ValueTask<ServiceResult> RegisterAsync<TEmployeeNames>(IRegisterEntity register, IEmployeeEntity<TEmployeeNames> employee)
            where TEmployeeNames : IEmployeeNameEntity;
    }
}