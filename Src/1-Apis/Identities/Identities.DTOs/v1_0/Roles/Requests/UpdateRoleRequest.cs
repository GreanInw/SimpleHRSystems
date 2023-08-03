using HR.Common.DTOs.Identities.Roles;
using HR.Common.Libs.Webs.Attributes;
using HR.Common.Results;
using MediatR;
using System.ComponentModel;

namespace Identities.DTOs.v1_0.Roles.Requests
{
    public class UpdateRoleRequest : IUpdateRoleEntity, IRequest<ServiceResult>
    {
        [DisplayName("id")]
        [SnakeCaseFromForm(nameof(Id))]
        public Guid Id { get; set; }

        [SnakeCaseFromForm(nameof(Description))]
        public string Description { get; set; }

        [SnakeCaseFromForm(nameof(IsActive))]
        public bool IsActive { get; set; }
    }
}
