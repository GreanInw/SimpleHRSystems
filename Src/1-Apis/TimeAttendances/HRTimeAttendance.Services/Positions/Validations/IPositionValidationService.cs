using HR.Common.Results;

namespace HRTimeAttendance.Services.Positions.Validations
{
    public interface IPositionValidationService
    {
        ValueTask<ServiceResult> ValidateIdAsync(Guid id);
    }
}
