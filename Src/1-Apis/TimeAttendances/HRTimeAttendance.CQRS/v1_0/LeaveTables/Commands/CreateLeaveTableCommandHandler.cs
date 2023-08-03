using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.LeaveTables.Requests;
using HRTimeAttendance.Services.LeaveTables.Commands;
using MediatR;

namespace HRTimeAttendance.CQRS.v1_0.LeaveTables.Commands
{
    public class CreateLeaveTableCommandHandler : IRequestHandler<CreateLeaveTableRequest, ServiceResult>
    {
        private readonly ILeaveTableCommandService _leaveTableCommandService;

        public CreateLeaveTableCommandHandler(ILeaveTableCommandService leaveTableCommandService)
        {
            _leaveTableCommandService = leaveTableCommandService;
        }

        public async Task<ServiceResult> Handle(CreateLeaveTableRequest request, CancellationToken cancellationToken)
            => await _leaveTableCommandService.CreateAsync(request);
    }
}
