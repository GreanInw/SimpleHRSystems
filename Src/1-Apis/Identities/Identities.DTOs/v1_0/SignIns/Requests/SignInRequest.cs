using HR.Common.DTOs.Identities.SignIns.Bases;
using HR.Common.Libs.Webs.Attributes;
using HR.Common.Results;
using Identities.DTOs.v1_0.SignIns.Responses;
using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Identities.DTOs.v1_0.SignIns.Requests
{
    public class SignInRequest : ISignInEntity, IRequest<ServiceResult<SignInResponse>>
    {
        [Required, DisplayName("username")]
        [SnakeCaseFromForm(nameof(Username))]
        public string Username { get; set; }

        [Required, DisplayName("password")]
        [SnakeCaseFromForm(nameof(Password))]
        public string Password { get; set; }
    }
}