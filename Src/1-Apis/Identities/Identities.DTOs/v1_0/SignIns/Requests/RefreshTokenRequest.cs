using HR.Common.DTOs.Identities.SignIns.Bases;
using HR.Common.Libs.Webs.Attributes;
using HR.Common.Results;
using Identities.DTOs.v1_0.SignIns.Responses;
using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Identities.DTOs.v1_0.SignIns.Requests
{
    public class RefreshTokenRequest : IRefreshTokenEntity, IRequest<ServiceResult<RefreshTokenResponse>>
    {
        [Required, DisplayName("access token")]
        [SnakeCaseFromForm(nameof(AccessToken))]
        public string AccessToken { get; set; }

        [Required, DisplayName("refresh token")]
        [SnakeCaseFromForm(nameof(RefreshToken))]
        public string RefreshToken { get; set; }
    }
}
