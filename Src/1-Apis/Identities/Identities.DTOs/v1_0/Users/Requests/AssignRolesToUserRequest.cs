using HR.Common.DTOs.Identities.Users;
using HR.Common.Results;
using MediatR;
using Newtonsoft.Json;

namespace Identities.DTOs.v1_0.Users.Requests
{
    [JsonObject]
    public class AssignRolesToUserRequest : IAssignRolesToUserEntity
        , IRequest<ServiceResult>
    {
        public Guid UserId { get; set; }
        public IEnumerable<Guid> RoleIds { get; set; }
    }
}
