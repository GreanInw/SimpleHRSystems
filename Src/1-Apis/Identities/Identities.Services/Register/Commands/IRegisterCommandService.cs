using HR.Common.DTOs.Identities.Registers;
using HR.Common.Results;
using HR.Common.Services.Bases;

namespace Identities.Services.Register.Commands
{
    public interface IRegisterCommandService : IBaseService
    {
        ValueTask<ServiceResult> RegisterAsync(IRegisterEntity entity);
    }
}
