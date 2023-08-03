using HR.Common.DTOs.Identities.Roles.Responses;
using HR.Common.Results;

namespace Identities.Services.Roles.Queries
{
    public interface IRoleQueryService
    {
        ValueTask<ServiceResult<IEnumerable<T>>> GetAllAsync<T>() where T : RoleBaseResponse, new();
    }
}
