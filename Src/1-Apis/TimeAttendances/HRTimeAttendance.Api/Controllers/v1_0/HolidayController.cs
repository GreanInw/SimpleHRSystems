using HR.Common.Libs.Controllers;
using HR.Common.Results;
using HRTimeAttendance.DTOs.v1_0.Holidays.Requests;
using HRTimeAttendance.DTOs.v1_0.Holidays.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRTimeAttendance.Api.Controllers.v1_0
{
    /// <inheritdoc/>
    [ApiController, ApiVersion("1.0"), Authorize]
    [Route("api/v{version:apiVersion}/holidays")]
    public class HolidayController : BaseInternalController
    {
        public HolidayController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult<IEnumerable<GetHolidayResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult<IEnumerable<GetHolidayResponse>>))]
        public async Task<IActionResult> Get([FromQuery] GetHolidayRequest request)
            => await ReturnResponseAsync<GetHolidayRequest, IEnumerable<GetHolidayResponse>>(request);

        /// <summary>
        /// Create holiday.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult))]
        public async Task<IActionResult> Create([FromForm] CreateHolidayRequest request)
            => await ReturnResponseAsync(request);

        /// <summary>
        /// Update holiday.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResult))]
        public async Task<IActionResult> Update([FromForm] UpdateHolidayRequest request)
            => await ReturnResponseAsync(request);
    }
}