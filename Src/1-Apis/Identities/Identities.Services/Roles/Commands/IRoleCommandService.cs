using HR.Common.DTOs.Identities.Roles;
using HR.Common.Results;

namespace Identities.Services.Roles.Commands
{
    public interface IRoleCommandService
    {
        ValueTask<ServiceResult> CreateAsync(ICreateRoleEntity entity);
        ValueTask<ServiceResult> UpdateAsync(IUpdateRoleEntity entity);
    }
}
