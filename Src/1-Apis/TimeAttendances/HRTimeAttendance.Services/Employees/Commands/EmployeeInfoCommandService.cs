using HR.Common.DALs.Repositories.HumanResources.Employees.Commands;
using HR.Common.DALs.UnitOfWorks;
using HR.Common.DTOs.HumanResources.Employees;
using HR.Common.DTOs.HumanResources.Employees.EmployeeNames;
using HR.Common.Results;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;

namespace HRTimeAttendance.Services.Employees.Commands
{
    public class EmployeeInfoCommandService : HRCommonCommandService, IEmployeeInfoCommandService
    {
        private readonly IEmployeeCommandRepository _employeeCommandRepository;

        public EmployeeInfoCommandService(IHttpContextAccessor httpContextAccessor, IHRUnitOfWork unitOfWork
            , IEmployeeCommandRepository employeeCommandRepository)
            : base(httpContextAccessor, unitOfWork)
        {
            _employeeCommandRepository = employeeCommandRepository;
        }

        public ValueTask<ServiceResult> UpdateAsync<TEmployeeNames>(Guid employeeId, IEmployeeEntity<TEmployeeNames> employee)
            where TEmployeeNames : IEmployeeNameEntity
        {
            throw new NotImplementedException();
        }
    }
}