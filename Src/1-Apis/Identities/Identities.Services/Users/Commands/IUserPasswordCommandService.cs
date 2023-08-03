using HR.Common.DTOs.Identities.Users;
using HR.Common.Results;

namespace Identities.Services.Passwords.Commands
{
    public interface IUserPasswordCommandService
    {
        ValueTask<ServiceResult> ChangePasswordAsync(IChangePasswordEntity entity);
        ValueTask<ServiceResult> ResetPasswordAsync(IResetPasswordEntity entity);
    }
}
