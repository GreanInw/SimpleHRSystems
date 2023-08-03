using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.Holidays.Requests;
using HRTimeAttendance.Services.Holidays.Commands;
using MediatR;

namespace HRTimeAttendance.CQRS.v1_0.Holidays.Commands
{
    public class UpdateHolidayCommandHandler : IRequestHandler<UpdateHolidayRequest, ServiceResult>
    {
        private readonly IHolidayCommandService _holidayCommandService;

        public UpdateHolidayCommandHandler(IHolidayCommandService holidayCommandService)
        {
            _holidayCommandService = holidayCommandService;
        }

        public async Task<ServiceResult> Handle(UpdateHolidayRequest request, CancellationToken cancellationToken)
            => await _holidayCommandService.UpdateAsync(request);
    }
}
