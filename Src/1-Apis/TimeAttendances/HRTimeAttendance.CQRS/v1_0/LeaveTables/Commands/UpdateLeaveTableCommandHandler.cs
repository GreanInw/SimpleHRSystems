using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.LeaveTables.Requests;
using HRTimeAttendance.Services.LeaveTables.Commands;
using MediatR;

namespace HRTimeAttendance.CQRS.v1_0.LeaveTables.Commands
{
    public class UpdateLeaveTableCommandHandler : IRequestHandler<UpdateLeaveTableRequest, ServiceResult>
    {
        private readonly ILeaveTableCommandService _leaveTableCommandService;

        public UpdateLeaveTableCommandHandler(ILeaveTableCommandService leaveTableCommandService)
        {
            _leaveTableCommandService = leaveTableCommandService;
        }

        public async Task<ServiceResult> Handle(UpdateLeaveTableRequest request, CancellationToken cancellationToken)
            => await _leaveTableCommandService.UpdateAsync(request);
    }
}
