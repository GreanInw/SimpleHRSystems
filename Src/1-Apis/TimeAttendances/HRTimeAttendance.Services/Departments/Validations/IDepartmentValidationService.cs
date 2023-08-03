using HR.Common.Results;

namespace HRTimeAttendance.Services.Departments.Validations
{
    public interface IDepartmentValidationService
    {
        ValueTask<ServiceResult> ValidateIdAsync(Guid id);
    }

}
