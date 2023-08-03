using HR.Common.Constants;
using HR.Common.Identities.Attributes;
using HR.Common.Libs.Controllers;
using HR.Common.Results;
using Identities.DTOs.v1_0.Roles.Requests;
using Identities.DTOs.v1_0.Roles.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identities.Api.Controllers.v1_0
{
    [ApiController, ApiVersion("1.0"), Authorize]
    [Route("api/v{version:apiVersion}/roles")]
    public class RoleController : BaseInternalController
    {
        public RoleController(IMediator mediator) : base(mediator)
        { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult<IEnumerable<GetRoleResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult<IEnumerable<GetRoleResponse>>))]
        public async Task<IActionResult> GetRoles()
            => await ReturnResponseAsync<GetRoleRequest, IEnumerable<GetRoleResponse>>(new GetRoleRequest());

        [HttpPost, RoleAuthorize(RolesConstants.SystemAdmins)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult))]
        public async Task<IActionResult> CreateRole([FromForm] CreateRoleRequest request)
            => await ReturnResponseAsync(request);

        [HttpPut, RoleAuthorize(RolesConstants.SystemAdmins)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult))]
        public async Task<IActionResult> UpdateRole([FromForm] UpdateRoleRequest request)
            => await ReturnResponseAsync(request);
    }
}
