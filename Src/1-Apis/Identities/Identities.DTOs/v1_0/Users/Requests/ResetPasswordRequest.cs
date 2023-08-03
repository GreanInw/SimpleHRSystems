using HR.Common.DTOs.Identities.Users;
using HR.Common.Libs.Webs.Attributes;
using HR.Common.Results;
using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Identities.DTOs.v1_0.Users.Requests
{
    public class ResetPasswordRequest : IResetPasswordEntity, IRequest<ServiceResult>
    {
        [Required, DisplayName("username")]
        [SnakeCaseFromForm(nameof(Username))]
        public string Username { get; set; }

        [Required, DisplayName("new password")]
        [SnakeCaseFromForm(nameof(NewPassword))]
        public string NewPassword { get; set; }
    }
}
