using HR.Common.DTOs.HumanResources.Positions;
using HR.Common.Results;

namespace HRTimeAttendance.Services.Positions.Commands
{
    public interface IPositionCommandService
    {
        ValueTask<ServiceResult> CreateAsync(ICreatePositionEntity entity);
        ValueTask<ServiceResult> UpdateAsync(IUpdatePositionEntity entity);
    }
}
