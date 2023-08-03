using HR.Common.DTOs.HumanResources.Positions.Responses;
using HR.Common.Results;

namespace HRTimeAttendance.Services.Positions.Queries
{
    public interface IPositionQueryService
    {
        ValueTask<ServiceResult<IEnumerable<T>>> GetAsync<T>() where T : PositionBaseResponse, new();
    }
}
