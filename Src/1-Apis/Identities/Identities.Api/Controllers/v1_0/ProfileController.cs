using HR.Common.Libs.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identities.Api.Controllers.v1_0
{
    [ApiController, ApiVersion("1.0"), Authorize]
    [Route("api/v{version:apiVersion}/profile")]
    public class ProfileController : BaseInternalController
    {
        public ProfileController(IMediator mediator) : base(mediator)
        { }

        [HttpGet]
        public IActionResult GetProfile()
        {
            //ClaimTypes.e
            return Ok(User.Claims.Select(s => new { s.Type, s.Value }));
        }
    }
}