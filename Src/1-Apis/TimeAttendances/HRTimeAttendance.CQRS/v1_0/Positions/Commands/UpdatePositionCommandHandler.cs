using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.Positions.Requests;
using HRTimeAttendance.Services.Positions.Commands;
using MediatR;

namespace HRTimeAttendance.CQRS.v1_0.Positions.Commands
{
    public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionRequest, ServiceResult>
    {
        private readonly IPositionCommandService _positionCommandService;

        public UpdatePositionCommandHandler(IPositionCommandService positionCommandService)
        {
            _positionCommandService = positionCommandService;
        }

        public async Task<ServiceResult> Handle(UpdatePositionRequest request, CancellationToken cancellationToken)
            => await _positionCommandService.UpdateAsync(request);
    }
}
