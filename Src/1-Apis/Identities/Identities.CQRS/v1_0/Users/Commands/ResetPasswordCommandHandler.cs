using HR.Common.Results;
using Identities.DTOs.v1_0.Users.Requests;
using Identities.Services.Passwords.Commands;
using MediatR;

namespace Identities.CQRS.v1_0.Users.Commands
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordRequest, ServiceResult>
    {
        private readonly IUserPasswordCommandService _passwordCommandService;

        public ResetPasswordCommandHandler(IUserPasswordCommandService passwordCommandService)
        {
            _passwordCommandService = passwordCommandService;
        }

        public async Task<ServiceResult> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
            => await _passwordCommandService.ResetPasswordAsync(request);
    }
}
