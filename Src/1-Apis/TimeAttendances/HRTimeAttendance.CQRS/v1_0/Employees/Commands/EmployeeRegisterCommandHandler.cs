using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.Employees.Requests;
using HRTimeAttendance.Services.Employees.Commands;
using MediatR;

namespace HRTimeAttendance.CQRS.v1_0.Employees.Commands
{
    public class EmployeeRegisterCommandHandler : IRequestHandler<EmployeeRegisterRequest, ServiceResult>
    {
        private readonly IEmployeeRegisterCommandService _employeeRegisterCommandService;

        public EmployeeRegisterCommandHandler(IEmployeeRegisterCommandService employeeRegisterCommandService)
        {
            _employeeRegisterCommandService = employeeRegisterCommandService;
        }

        public async Task<ServiceResult> Handle(EmployeeRegisterRequest request, CancellationToken cancellationToken)
            => await _employeeRegisterCommandService.RegisterAsync(request.Register
                , request.Employee);
    }
}