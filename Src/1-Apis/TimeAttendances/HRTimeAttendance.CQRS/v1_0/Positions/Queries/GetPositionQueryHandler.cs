using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.Positions.Requests;
using HRTimeAttendance.DTOs.v1_0.Positions.Respones;
using HRTimeAttendance.Services.Positions.Queries;
using MediatR;

namespace HRTimeAttendance.CQRS.v1_0.Positions.Queries
{
    public class GetPositionQueryHandler : IRequestHandler<GetPositionRequest, ServiceResult<IEnumerable<GetPositionResponse>>>
    {
        private readonly IPositionQueryService _positionQueryService;

        public GetPositionQueryHandler(IPositionQueryService positionQueryService)
        {
            _positionQueryService = positionQueryService;
        }

        public async Task<ServiceResult<IEnumerable<GetPositionResponse>>> Handle(GetPositionRequest request, CancellationToken cancellationToken)
            => await _positionQueryService.GetAsync<GetPositionResponse>();
    }
}
