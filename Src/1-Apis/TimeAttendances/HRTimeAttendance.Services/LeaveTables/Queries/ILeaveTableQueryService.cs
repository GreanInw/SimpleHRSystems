using HR.Common.DTOs.HumanResources.LeaveTables.Responses;
using HR.Common.Results;

namespace HRTimeAttendance.Services.LeaveTables.Queries
{
    public interface ILeaveTableQueryService
    {
        ValueTask<ServiceResult<IEnumerable<T>>> GetAllAsync<T>() where T : LeaveTableBaseResponse, new();
    }
}
