using HR.Common.DTOs.Identities.Users;
using HR.Common.Results;

namespace Identities.Services.Users.Commands
{
    public interface IUserCommandService
    {
        ValueTask<ServiceResult> ActiveOrInactiveAsync(string username, bool isActive);
        ValueTask<ServiceResult> AssignRolesAsync(IAssignRolesToUserEntity entity);
    }
}
