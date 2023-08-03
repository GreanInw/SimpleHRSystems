using HR.Common.Libs.Controllers;
using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.Employees.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRTimeAttendance.Api.Controllers.v1_0
{
    [ApiController, ApiVersion("1.0"), Authorize]
    [Route("api/v{version:apiVersion}/employee")]
    public class EmployeeController : BaseInternalController
    {
        public EmployeeController(IMediator mediator) : base(mediator) { }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult))]
        public async Task<IActionResult> EmployeeRegister([FromBody] EmployeeRegisterRequest request)
            => await ReturnResponseAsync(request);
    }
}
