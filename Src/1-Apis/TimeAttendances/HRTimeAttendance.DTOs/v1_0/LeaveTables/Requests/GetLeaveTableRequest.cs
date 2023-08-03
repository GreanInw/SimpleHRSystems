using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.LeaveTables.Responses;
using MediatR;

namespace HRTimeAttendance.DTOs.v1_0.LeaveTables.Requests
{
    public class GetLeaveTableRequest : IRequest<ServiceResult<IEnumerable<LeaveTableResponse>>>
    { }
}
