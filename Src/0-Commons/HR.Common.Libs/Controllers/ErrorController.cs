using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace HR.Common.Libs.Controllers
{
    [ApiController, ApiVersion("1.0")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/v{version:apiVersion}/error")]
        public IActionResult Error() => Problem();

        [Route("/v{version:apiVersion}/error-dev")]
        public IActionResult ErrorDev([FromServices] IWebHostEnvironment environment)
        {
            if (environment.IsProduction())
            {
                throw new InvalidOperationException("This shouldn't be invoked in non-development, non-staging environments.");
            }

            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return Problem(detail: exceptionHandlerFeature?.Error?.StackTrace
                , title: exceptionHandlerFeature?.Error?.Message);
        }
    }
}
