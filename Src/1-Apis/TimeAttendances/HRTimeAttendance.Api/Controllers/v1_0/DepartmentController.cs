using HR.Common.Libs.Controllers;
using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.Departments.Requests;
using HRTimeAttendance.DTOs.v1_0.Departments.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRTimeAttendance.Api.Controllers.v1_0
{
    [ApiController, ApiVersion("1.0"), Authorize]
    [Route("api/v{version:apiVersion}/departments")]
    public class DepartmentController : BaseInternalController
    {
        public DepartmentController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult<IEnumerable<GetDepartmentResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult<IEnumerable<GetDepartmentResponse>>))]
        public async Task<IActionResult> Get([FromQuery] GetDepartmentRequest request)
            => await ReturnResponseAsync<GetDepartmentRequest, IEnumerable<GetDepartmentResponse>>(request);

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult))]
        public async Task<IActionResult> Create([FromForm] CreateDepartmentRequest request)
            => await ReturnResponseAsync(request);

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult))]
        public async Task<IActionResult> Update([FromForm] UpdateDepartmentRequest request)
            => await ReturnResponseAsync(request);
    }
}