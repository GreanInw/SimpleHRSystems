using HR.Common.Libs.Controllers;
using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.LeaveTables.Requests;
using HRTimeAttendance.DTOs.v1_0.LeaveTables.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRTimeAttendance.Api.Controllers.v1_0
{
    /// <inheritdoc/>
    [ApiController, ApiVersion("1.0"), Authorize]
    [Route("api/v{version:apiVersion}/leaves")]
    public class LeaveController : BaseInternalController
    {
        /// <inheritdoc/>
        public LeaveController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Get leave master data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult<IEnumerable<LeaveTableResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult<IEnumerable<LeaveTableResponse>>))]
        public async Task<IActionResult> Get()
            => await ReturnResponseAsync<GetLeaveTableRequest, IEnumerable<LeaveTableResponse>>(new GetLeaveTableRequest());

        /// <summary>
        /// Create leave master data.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult))]
        public async Task<IActionResult> CreateLeaveTable([FromForm] CreateLeaveTableRequest request)
            => await ReturnResponseAsync(request);

        /// <summary>
        /// Update leave master data.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult))]
        public async Task<IActionResult> UpdateLeaveTable([FromForm] UpdateLeaveTableRequest request)
            => await ReturnResponseAsync(request);
    }
}