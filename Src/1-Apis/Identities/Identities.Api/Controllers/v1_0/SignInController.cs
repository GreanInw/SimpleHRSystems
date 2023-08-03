using HR.Common.Libs.Controllers;
using HR.Common.Results;
using Identities.DTOs.v1_0.SignIns.Requests;
using Identities.DTOs.v1_0.SignIns.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identities.Api.Controllers.v1_0
{
    [ApiController, ApiVersion("1.0"), AllowAnonymous]
    [Route("api/v{version:apiVersion}/signin")]
    public class SignInController : BaseInternalController
    {
        public SignInController(IMediator mediator) : base(mediator)
        { }

        /// <summary>
        /// Sign in.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult<SignInResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult<SignInResponse>))]
        public async Task<IActionResult> SignIn([FromForm] SignInRequest request)
            => await ReturnResponseAsync<SignInRequest, SignInResponse>(request);

        /// <summary>
        /// Refresh token.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult<RefreshTokenResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult<RefreshTokenResponse>))]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenRequest request)
            => await ReturnResponseAsync<RefreshTokenRequest, RefreshTokenResponse>(request);
    }
}