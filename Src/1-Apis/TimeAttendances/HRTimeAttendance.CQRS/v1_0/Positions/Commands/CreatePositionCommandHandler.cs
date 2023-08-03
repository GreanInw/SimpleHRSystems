using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.Positions.Requests;
using HRTimeAttendance.Services.Positions.Commands;
using MediatR;

namespace HRTimeAttendance.CQRS.v1_0.Positions.Commands
{
    public class CreatePositionCommandHandler : IRequestHandler<CreatePositionRequest, ServiceResult>
    {
        private readonly IPositionCommandService _positionCommandService;

        public CreatePositionCommandHandler(IPositionCommandService positionCommandService)
        {
            _positionCommandService = positionCommandService;
        }

        public async Task<ServiceResult> Handle(CreatePositionRequest request, CancellationToken cancellationToken)
            => await _positionCommandService.CreateAsync(request);
    }
}