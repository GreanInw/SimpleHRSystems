using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.Positions.Respones;
using MediatR;

namespace HRTimeAttendance.DTOs.v1_0.Positions.Requests
{
    public class GetPositionRequest : IRequest<ServiceResult<IEnumerable<GetPositionResponse>>> { }
}
