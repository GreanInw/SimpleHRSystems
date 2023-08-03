using HR.Common.Identities.Attributes;
using HR.Common.Identities.Extensions;
using HR.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HR.Common.Identities.Filters
{
    public class RoleAuthorizeFilter : IAuthorizationFilter
    {
        public RoleAuthorizeFilter(params string[] roles)
        {
            Roles = roles;
        }

        public string[] Roles { get; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.RouteData.IsErrorController()
                || context.ActionDescriptor.IsAllowAttributeOnAction<AllowRoleAuthorizeAttribute>())
            {
                return;
            }

            context.Result = context.HttpContext.User.Claims.GetRoleValues().Any(w => Roles.Contains(w))
                ? null : new UnauthorizedObjectResult(new ServiceResult(error: "Role unauthorized"));
        }
    }
}