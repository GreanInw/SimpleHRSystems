using Microsoft.IdentityModel.JsonWebTokens;

namespace HR.Common.Constants
{
    public class JwtIdentityConstants
    {
        public class ClaimNames
        {
            public const string OId = "oid";
            /// <summary>
            /// Username
            /// </summary>
            public const string Sub = JwtRegisteredClaimNames.Sub;
            public const string Aud = JwtRegisteredClaimNames.Aud;
            public const string Email = JwtRegisteredClaimNames.Email;
            public const string Jti = JwtRegisteredClaimNames.Jti;
            public const string Name = JwtRegisteredClaimNames.Name;
            public const string GivenName = JwtRegisteredClaimNames.GivenName;
            public const string FamilyName = JwtRegisteredClaimNames.FamilyName;
            public const string Exp = JwtRegisteredClaimNames.Exp;
            public const string Role = "roles";
            public const string EmployeeId = "emp_id";
        }
    }
}