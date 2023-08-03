using HR.Common.DTOs.HumanResources.Holidays;
using HR.Common.Results;

namespace HRTimeAttendance.Services.Holidays.Commands
{
    public interface IHolidayCommandService
    {
        ValueTask<ServiceResult> CreateAsync(ICreateHolidayEntity entity);
        ValueTask<ServiceResult> UpdateAsync(IUpdateHolidayEntity entity);
    }
}