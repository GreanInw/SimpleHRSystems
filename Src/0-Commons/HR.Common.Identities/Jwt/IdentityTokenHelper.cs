using HR.Common.Configurations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR.Common.Identities.Jwt
{
    public class IdentityTokenHelper
    {
        public IdentityTokenHelper(JwtConfigurations configuration, IdentitySettings identitySettings)
        {
            Configuration = configuration;
            IdentitySettings = identitySettings;
        }

        public JwtConfigurations Configuration { get; }
        public IdentitySettings IdentitySettings { get; }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Configuration.ExpiresInOfMinutes),
                Issuer = IdentitySettings.Issuer,
                Audience = IdentitySettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(GetKey()),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
            => Convert.ToBase64String(Encoding.ASCII.GetBytes(Guid.NewGuid().ToString()));

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(GetKey()),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        private byte[] GetKey() => Encoding.ASCII.GetBytes(IdentitySettings.SecretKey);
    }
}