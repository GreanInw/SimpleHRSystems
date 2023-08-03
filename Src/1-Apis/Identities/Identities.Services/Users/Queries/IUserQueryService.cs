using HR.Common.DTOs.Bases;
using HR.Common.DTOs.Identities.Users;
using HR.Common.DTOs.Identities.Users.Responses;
using HR.Common.Results;

namespace Identities.Services.Users.Queries
{
    public interface IUserQueryService
    {
        ValueTask<ServiceResultPaging<T>> GetAsync<T>(IGetUsersEntity entity, IPaging paging) 
            where T : UserBaseResponse, new();
    }
}