using HR.Common.DTOs.Identities.Roles;
using HR.Common.Libs.Webs.Attributes;
using HR.Common.Results;
using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Identities.DTOs.v1_0.Roles.Requests
{
    public class CreateRoleRequest : ICreateRoleEntity, IRequest<ServiceResult>
    {
        [Required, DisplayName("name")]
        [SnakeCaseFromForm(nameof(Name))]
        public string Name { get; set; }

        [SnakeCaseFromForm(nameof(Description))]
        public string Description { get; set; }
    }
}