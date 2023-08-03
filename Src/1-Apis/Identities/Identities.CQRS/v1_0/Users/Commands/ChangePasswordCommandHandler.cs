using HR.Common.Results;
using Identities.DTOs.v1_0.Users.Requests;
using Identities.Services.Passwords.Commands;
using MediatR;

namespace Identities.CQRS.v1_0.Users.Commands
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordRequest, ServiceResult>
    {
        private readonly IUserPasswordCommandService _passwordCommandService;

        public ChangePasswordCommandHandler(IUserPasswordCommandService passwordCommandService)
        {
            _passwordCommandService = passwordCommandService;
        }

        public async Task<ServiceResult> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
            => await _passwordCommandService.ChangePasswordAsync(request);
    }
}