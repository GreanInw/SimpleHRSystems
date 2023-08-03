using HR.Common.DTOs.HumanResources.Holidays;
using HR.Common.DTOs.HumanResources.Holidays.Responses;
using HR.Common.Results;

namespace HRTimeAttendance.Services.Holidays.Queries
{
    public interface IHolidayQueryService
    {
        ValueTask<ServiceResult<IEnumerable<T>>> GetAsync<T>(IGetHolidayEntity entity) 
            where T : HolidayBaseResponse, new();
    }
}