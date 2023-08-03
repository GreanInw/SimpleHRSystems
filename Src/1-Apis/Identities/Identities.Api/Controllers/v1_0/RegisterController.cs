using HR.Common.Libs.Controllers;
using HR.Common.Results;
using Identities.DTOs.v1_0.Registers.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identities.Api.Controllers.v1_0
{
    [ApiController, ApiVersion("1.0"), Authorize]
    [Route("api/v{version:apiVersion}/register")]
    public class RegisterController : BaseInternalController
    {
        public RegisterController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Register user.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult))]
        public async Task<IActionResult> Create([FromForm] RegisterRequest request)
            => await ReturnResponseAsync(request);
    }
}