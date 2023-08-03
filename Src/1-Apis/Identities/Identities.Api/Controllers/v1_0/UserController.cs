using HR.Common.Constants;
using HR.Common.Identities.Attributes;
using HR.Common.Libs.Controllers;
using HR.Common.Results;
using Identities.DTOs.v1_0.Users.Requests;
using Identities.DTOs.v1_0.Users.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identities.Api.Controllers.v1_0
{
    [ApiController, ApiVersion("1.0"), Authorize]
    [Route("api/v{version:apiVersion}/user")]
    public class UserController : BaseInternalController
    {
        public UserController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Change password.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("changepassword")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult))]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordRequest request)
            => await ReturnResponseAsync(request);

        /// <summary>
        /// Reset password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("resetpassword"), RoleAuthorize(RolesConstants.SystemAdmins, RolesConstants.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult))]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordRequest request)
            => await ReturnResponseAsync(request);

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResultPaging<GetUsersResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResultPaging<GetUsersResponse>))]
        public async Task<IActionResult> GetUsers([FromForm] GetUsersRequest request)
            => await ReturnResponsePagingAsync<GetUsersRequest, GetUsersResponse>(request);

        /// <summary>
        /// Assign roles to user.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("roles/assign"), RoleAuthorize(RolesConstants.SystemAdmins, RolesConstants.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult))]
        public async Task<IActionResult> AssignRolesToUser([FromBody] AssignRolesToUserRequest request)
            => await ReturnResponseAsync(request);
    }
}