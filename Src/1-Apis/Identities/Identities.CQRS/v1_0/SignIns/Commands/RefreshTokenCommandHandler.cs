using HR.Common.Results;
using Identities.DTOs.v1_0.SignIns.Requests;
using Identities.DTOs.v1_0.SignIns.Responses;
using Identities.Services.SignIn.Commands;
using MediatR;

namespace Identities.CQRS.v1_0.SignIns.Commands
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenRequest, ServiceResult<RefreshTokenResponse>>
    {
        private readonly ISignInCommandService _signInCommandService;

        public RefreshTokenCommandHandler(ISignInCommandService signInCommandService)
        {
            _signInCommandService = signInCommandService;
        }

        public async Task<ServiceResult<RefreshTokenResponse>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
            => await _signInCommandService.RefreshTokenAsync<RefreshTokenResponse>(request);
    }
}
