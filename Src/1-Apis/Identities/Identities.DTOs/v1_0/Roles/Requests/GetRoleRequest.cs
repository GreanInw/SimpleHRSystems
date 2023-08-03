using HR.Common.Results;
using Identities.DTOs.v1_0.Roles.Responses;
using MediatR;

namespace Identities.DTOs.v1_0.Roles.Requests
{
    public class GetRoleRequest : IRequest<ServiceResult<IEnumerable<GetRoleResponse>>> { }

}
