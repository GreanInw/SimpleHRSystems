using HR.Common.DTOs.Identities.Registers;
using HR.Common.Libs.Webs.Attributes;
using HR.Common.Results;
using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Identities.DTOs.v1_0.Registers.Requests
{
    public class RegisterRequest : IRegisterEntity, IRequest<ServiceResult>
    {
        [SnakeCaseFromForm(nameof(Username))]
        [Required, DisplayName("username")]
        public string Username { get; set; }

        [SnakeCaseFromForm(nameof(Password))]
        [Required, DisplayName("password")]
        public string Password { get; set; }

        [SnakeCaseFromForm(nameof(ConfirmPassword))]
        [Required, DisplayName("confirm password")]
        [Compare("Password", ErrorMessage = "The 'password' and  'confirm password' do not match")]
        public string ConfirmPassword { get; set; }
    }
}