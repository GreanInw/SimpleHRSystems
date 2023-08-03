using HR.Common.Identities.Filters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HR.Common.Identities.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class RoleAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public RoleAuthorizeAttribute(params string[] roles)
        {
            Roles = roles;
        }

        public string[] Roles { get; }

        public void OnAuthorization(AuthorizationFilterContext context)
            => new RoleAuthorizeFilter(Roles).OnAuthorization(context);
    }
}