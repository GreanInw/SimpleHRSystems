using HR.Common.Results;
using Identities.DTOs.v1_0.Roles.Requests;
using Identities.Services.Roles.Commands;
using MediatR;

namespace Identities.CQRS.v1_0.Roles.Commands
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleRequest, ServiceResult>
    {
        private readonly IRoleCommandService _roleCommandService;

        public UpdateRoleCommandHandler(IRoleCommandService roleCommandService)
        {
            _roleCommandService = roleCommandService;
        }

        public async Task<ServiceResult> Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
            => await _roleCommandService.UpdateAsync(request);
    }
}
