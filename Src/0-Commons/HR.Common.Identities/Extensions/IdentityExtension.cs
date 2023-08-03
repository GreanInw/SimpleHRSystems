using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace HR.Common.Identities.Extensions
{
    public static class IdentityExtension
    {
        public static bool IsErrorController(this RouteData route)
            => route.Values.Any(w => w.Value.ToString().ToLower().Contains("error"));

        public static bool IsAllowAttributeOnAction<TAttribute>(this ActionDescriptor actionDescriptor) where TAttribute : Attribute
            => actionDescriptor.EndpointMetadata.Any(w => w.GetType() == typeof(TAttribute));
    }
}