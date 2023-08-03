using HR.Common.Constants;
using System.Security.Claims;

namespace HR.Common.Identities.Extensions
{
    public static class ClaimsExtension
    {
        public static string GetOIdValue(this IEnumerable<Claim> claims)
            => claims.FirstOrDefault(w => w.Type == JwtIdentityConstants.ClaimNames.OId)?.Value;

        public static string GetEmailValue(this IEnumerable<Claim> claims)
            => claims.FirstOrDefault(w => w.Type == JwtIdentityConstants.ClaimNames.Email)?.Value;

        public static string GetUsernameValue(this IEnumerable<Claim> claims)
            => claims.FirstOrDefault(w => w.Type == JwtIdentityConstants.ClaimNames.Sub)?.Value;

        public static IEnumerable<string> GetRoleValues(this IEnumerable<Claim> claims)
            => claims.Where(w => w.Type == JwtIdentityConstants.ClaimNames.Role).Select(s => s.Value);

        public static string GetNameValue(this IEnumerable<Claim> claims)
            => claims.FirstOrDefault(w => w.Type == JwtIdentityConstants.ClaimNames.Name)?.Value;
    }
}
