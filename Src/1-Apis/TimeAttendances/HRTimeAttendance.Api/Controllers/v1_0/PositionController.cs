using HR.Common.Libs.Controllers;
using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.Positions.Requests;
using HRTimeAttendance.DTOs.v1_0.Positions.Respones;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRTimeAttendance.Api.Controllers.v1_0
{

    [ApiController, ApiVersion("1.0"), Authorize]
    [Route("api/v{version:apiVersion}/positions")]
    public class PositionController : BaseInternalController
    {
        public PositionController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult<IEnumerable<GetPositionResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult<IEnumerable<GetPositionResponse>>))]
        public async Task<IActionResult> Get()
            => await ReturnResponseAsync<GetPositionRequest, IEnumerable<GetPositionResponse>>(new GetPositionRequest());

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult))]
        public async Task<IActionResult> Create([FromForm] CreatePositionRequest request)
            => await ReturnResponseAsync(request);

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult))]
        public async Task<IActionResult> Update([FromForm] UpdatePositionRequest request)
            => await ReturnResponseAsync(request);
    }
}