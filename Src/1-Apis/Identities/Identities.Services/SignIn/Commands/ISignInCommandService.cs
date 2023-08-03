using HR.Common.DTOs.Identities.SignIns.Bases;
using HR.Common.DTOs.Identities.SignIns.Responses;
using HR.Common.Results;
using HR.Common.Services.Bases;

namespace Identities.Services.SignIn.Commands
{
    public interface ISignInCommandService : IBaseService
    {
        ValueTask<ServiceResult<T>> SignInAsync<T>(ISignInEntity entity)
            where T : SignInBaseResponse, new();
        ValueTask<ServiceResult<T>> RefreshTokenAsync<T>(IRefreshTokenEntity entity)
            where T : SignInBaseResponse, new();
    }
}
