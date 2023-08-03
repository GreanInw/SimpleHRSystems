using HR.Common.DTOs.HumanResources.Holidays;
using HR.Common.Libs.Webs.Attributes;
using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.Holidays.Responses;
using MediatR;

namespace HRTimeAttendance.DTOs.v1_0.Holidays.Requests
{
    public class GetHolidayRequest : IGetHolidayEntity, IRequest<ServiceResult<IEnumerable<GetHolidayResponse>>>
    {
        [SnakeCaseFromQuery(nameof(Year))]
        public int? Year { get; set; }
        [SnakeCaseFromQuery(nameof(LanguageId))]
        public int? LanguageId { get; set; }
    }
}