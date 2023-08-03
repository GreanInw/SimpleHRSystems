using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.Holidays.Requests;
using HRTimeAttendance.DTOs.v1_0.Holidays.Responses;
using HRTimeAttendance.Services.Holidays.Queries;
using MediatR;

namespace HRTimeAttendance.CQRS.v1_0.Holidays.Queries
{
    public class GetHolidayQueryHandler : IRequestHandler<GetHolidayRequest, ServiceResult<IEnumerable<GetHolidayResponse>>>
    {
        private readonly IHolidayQueryService _holidayQueryService;

        public GetHolidayQueryHandler(IHolidayQueryService holidayQueryService)
        {
            _holidayQueryService = holidayQueryService;
        }

        public async Task<ServiceResult<IEnumerable<GetHolidayResponse>>> Handle(GetHolidayRequest request, CancellationToken cancellationToken)
            => await _holidayQueryService.GetAsync<GetHolidayResponse>(request);
    }
}
