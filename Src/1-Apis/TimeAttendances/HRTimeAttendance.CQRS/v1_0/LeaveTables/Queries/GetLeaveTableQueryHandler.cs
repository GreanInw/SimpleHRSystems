using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.LeaveTables.Requests;
using HRTimeAttendance.DTOs.v1_0.LeaveTables.Responses;
using HRTimeAttendance.Services.LeaveTables.Queries;
using MediatR;

namespace HRTimeAttendance.CQRS.v1_0.LeaveTables.Queries
{
    public class GetLeaveTableQueryHandler : IRequestHandler<GetLeaveTableRequest, ServiceResult<IEnumerable<LeaveTableResponse>>>
    {
        private readonly ILeaveTableQueryService _leaveTableQueryService;

        public GetLeaveTableQueryHandler(ILeaveTableQueryService leaveTableQueryService)
        {
            _leaveTableQueryService = leaveTableQueryService;
        }

        public async Task<ServiceResult<IEnumerable<LeaveTableResponse>>> Handle(GetLeaveTableRequest request
            , CancellationToken cancellationToken)
            => await _leaveTableQueryService.GetAllAsync<LeaveTableResponse>();
    }
}
