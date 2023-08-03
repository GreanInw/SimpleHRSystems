using HR.Common.Results;
using Identities.DTOs.v1_0.Roles.Requests;
using Identities.DTOs.v1_0.Roles.Responses;
using Identities.Services.Roles.Queries;
using MediatR;

namespace Identities.CQRS.v1_0.Roles.Queries
{
    public class GetRolesQueryHandler : IRequestHandler<GetRoleRequest, ServiceResult<IEnumerable<GetRoleResponse>>>
    {
        private readonly IRoleQueryService _roleQueryService;

        public GetRolesQueryHandler(IRoleQueryService roleQueryService)
        {
            _roleQueryService = roleQueryService;
        }

        public async Task<ServiceResult<IEnumerable<GetRoleResponse>>> Handle(GetRoleRequest request, CancellationToken cancellationToken)
            => await _roleQueryService.GetAllAsync<GetRoleResponse>();
    }
}
