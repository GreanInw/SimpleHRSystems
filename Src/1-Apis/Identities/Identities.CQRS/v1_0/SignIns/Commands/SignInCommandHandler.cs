using HR.Common.Results;
using Identities.DTOs.v1_0.SignIns.Requests;
using Identities.DTOs.v1_0.SignIns.Responses;
using Identities.Services.SignIn.Commands;
using MediatR;

namespace Identities.CQRS.v1_0.SignIns.Commands
{
    public class SignInCommandHandler : IRequestHandler<SignInRequest, ServiceResult<SignInResponse>>
    {
        private readonly ISignInCommandService _signInCommandService;

        public SignInCommandHandler(ISignInCommandService signInCommandService)
        {
            _signInCommandService = signInCommandService;
        }

        public async Task<ServiceResult<SignInResponse>> Handle(SignInRequest request, CancellationToken cancellationToken)
            => await _signInCommandService.SignInAsync<SignInResponse>(request);
    }
}