using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.Holidays.Requests;
using HRTimeAttendance.Services.Holidays.Commands;
using MediatR;

namespace HRTimeAttendance.CQRS.v1_0.Holidays.Commands
{
    public class CreateHolidayCommandHandler : IRequestHandler<CreateHolidayRequest, ServiceResult>
    {
        private readonly IHolidayCommandService _holidayCommandService;

        public CreateHolidayCommandHandler(IHolidayCommandService holidayCommandService)
        {
            _holidayCommandService = holidayCommandService;
        }

        public async Task<ServiceResult> Handle(CreateHolidayRequest request, CancellationToken cancellationToken)
            => await _holidayCommandService.CreateAsync(request);
    }
}