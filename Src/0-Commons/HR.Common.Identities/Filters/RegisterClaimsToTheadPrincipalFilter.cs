using HR.Common.Identities.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HR.Common.Identities.Filters
{
    public class RegisterClaimsToTheadPrincipalFilter : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        { }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (context.HttpContext.User is null
                || !context.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            ThreadPrincipalHelper.Register(context.HttpContext.User);
        }
    }
}