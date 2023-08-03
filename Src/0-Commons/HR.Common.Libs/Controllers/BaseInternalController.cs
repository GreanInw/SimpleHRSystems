using HR.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.Common.Libs.Controllers
{
    public class BaseInternalController : ControllerBase
    {
        public BaseInternalController(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected IMediator Mediator { get; }

        protected IActionResult ReturnResponse(ServiceResult serviceResult)
            => ReturnResponseByServiceResult(new ServiceResult<object>(serviceResult.IsSuccess, serviceResult.Result, serviceResult.Errors));

        protected IActionResult ReturnInvalidModelState()
            => BadRequest(new ServiceResult(GetErrorMessages()));

        protected IEnumerable<string> GetErrorMessages()
            => ModelState.Values.SelectMany(s => s.Errors).Select(s => s.ErrorMessage);

        #region ServiceResult
        protected async Task<IActionResult> ReturnResponseAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<ServiceResult<TResponse>>
            => ModelState.IsValid ? ReturnResponseByServiceResult(await Mediator.Send(request)) : ReturnInvalidModelState();

        protected async Task<IActionResult> ReturnResponseAsync<TRequest>(TRequest request)
            where TRequest : IRequest<ServiceResult>
            => ModelState.IsValid ? ReturnResponse(await Mediator.Send(request)) : ReturnInvalidModelState();

        protected IActionResult ReturnResponseByServiceResult<T>(ServiceResult<T> serviceResult)
           => serviceResult.IsSuccess ? Ok(serviceResult) : BadRequest(serviceResult);
        #endregion

        #region ServiceResultPaging
        protected async Task<IActionResult> ReturnResponsePagingAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<ServiceResultPaging<TResponse>>
            => ModelState.IsValid
                ? ReturnResponseByServiceResultPaging(await Mediator.Send(request))
                : ReturnInvalidModelState();

        protected IActionResult ReturnResponseByServiceResultPaging<T>(ServiceResultPaging<T> serviceResult)
            => serviceResult.IsSuccess ? Ok(serviceResult) : BadRequest(serviceResult);
        #endregion
    }
}