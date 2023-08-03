using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.Departments.Requests;
using HRTimeAttendance.DTOs.v1_0.Departments.Responses;
using HRTimeAttendance.Services.Departments.Queries;
using MediatR;

namespace HRTimeAttendance.CQRS.v1_0.Departments.Queries
{
    public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentRequest, ServiceResult<IEnumerable<GetDepartmentResponse>>>
    {
        private readonly IDepartmentQueryService _departmentQueryService;

        public GetDepartmentQueryHandler(IDepartmentQueryService departmentQueryService)
        {
            _departmentQueryService = departmentQueryService;
        }

        public async Task<ServiceResult<IEnumerable<GetDepartmentResponse>>> Handle(GetDepartmentRequest request, CancellationToken cancellationToken)
            => await _departmentQueryService.GetAsync<GetDepartmentResponse>(request);
    }
}