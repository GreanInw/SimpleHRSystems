using HR.Common.Results;
using Identities.DTOs.v1_0.Roles.Requests;
using Identities.Services.Roles.Commands;
using MediatR;

namespace Identities.CQRS.v1_0.Roles.Commands
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleRequest, ServiceResult>
    {
        private readonly IRoleCommandService _roleCommandService;

        public CreateRoleCommandHandler(IRoleCommandService roleCommandService)
        {
            _roleCommandService = roleCommandService;
        }

        public async Task<ServiceResult> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
            => await _roleCommandService.CreateAsync(request);
    }
}