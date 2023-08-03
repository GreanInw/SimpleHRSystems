using HR.Common.DTOs.HumanResources.LeaveTables;
using HR.Common.Results;

namespace HRTimeAttendance.Services.LeaveTables.Commands
{
    public interface ILeaveTableCommandService
    {
        ValueTask<ServiceResult> CreateAsync(ICreateLeaveTableEntity entity);
        ValueTask<ServiceResult> UpdateAsync(IUpdateLeaveTableEntity entity);
    }
}