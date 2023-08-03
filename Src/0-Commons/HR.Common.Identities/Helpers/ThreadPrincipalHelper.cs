using HR.Common.Identities.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Principal;

namespace HR.Common.Identities.Helpers
{
    public class ThreadPrincipalHelper
    {
        public static void Register(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal is null || claimsPrincipal.Claims is null
                || !claimsPrincipal.Claims.Any())
            {
                throw new ArgumentNullException(nameof(claimsPrincipal));
            }

            var identity = new GenericIdentity(claimsPrincipal.Claims.GetUsernameValue())
            {
                Actor = new ClaimsIdentity(claimsPrincipal.Claims)
            };

            var roles = claimsPrincipal.Claims.GetRoleValues();
            var principal = new GenericPrincipal(identity
                , (roles is null || !roles.Any()) ? Array.Empty<string>() : roles.ToArray());

            principal.AddIdentity(new ClaimsIdentity(claimsPrincipal.Claims
                , claimsPrincipal.Identity.AuthenticationType));

            Thread.CurrentPrincipal = principal;
        }

        /// <summary>
        /// Register username for stamp data into auditable fields.
        /// </summary>
        /// <param name="username"></param>
        public static void Register(string username)
        {
            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
        }
    }
}