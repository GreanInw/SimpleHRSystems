using HR.Common.Identities.Helpers;
using HR.Common.Results;
using Identities.DTOs.v1_0.Registers.Requests;
using Identities.Services.Register.Commands;
using MediatR;

namespace Identities.CQRS.v1_0.Registers.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterRequest, ServiceResult>
    {
        private readonly IRegisterCommandService _registerCommandService;

        public RegisterCommandHandler(IRegisterCommandService registerCommandService)
        {
            _registerCommandService = registerCommandService;
        }

        public async Task<ServiceResult> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            ThreadPrincipalHelper.Register(request.Username);
            return await _registerCommandService.RegisterAsync(request);
        }
    }
}