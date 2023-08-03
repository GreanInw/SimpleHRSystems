using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.Departments.Requests;
using HRTimeAttendance.Services.Departments.Commands;
using MediatR;

namespace HRTimeAttendance.CQRS.v1_0.Departments.Commands
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentRequest, ServiceResult>
    {
        private readonly IDepartmentCommandService _departmentCommandService;

        public CreateDepartmentCommandHandler(IDepartmentCommandService departmentCommandService)
        {
            _departmentCommandService = departmentCommandService;
        }

        public async Task<ServiceResult> Handle(CreateDepartmentRequest request, CancellationToken cancellationToken)
            => await _departmentCommandService.CreateAsync(request);
    }
}