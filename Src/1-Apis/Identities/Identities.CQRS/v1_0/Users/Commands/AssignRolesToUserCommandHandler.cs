using HR.Common.Results;
using Identities.DTOs.v1_0.Users.Requests;
using Identities.Services.Users.Commands;
using MediatR;

namespace Identities.CQRS.v1_0.Users.Commands
{
    public class AssignRolesToUserCommandHandler : IRequestHandler<AssignRolesToUserRequest, ServiceResult>
    {
        private readonly IUserCommandService _userCommandService;

        public AssignRolesToUserCommandHandler(IUserCommandService userCommandService)
        {
            _userCommandService = userCommandService;
        }

        public async Task<ServiceResult> Handle(AssignRolesToUserRequest request, CancellationToken cancellationToken)
            => await _userCommandService.AssignRolesAsync(request);
    }
}
