using HR.Common.Services.AppConfigurations.Helpers.Attributes;

namespace HR.Common.Configurations
{
    public class JwtConfigurations
    {
        [AppConfigurationBinder("JwtExpiresInOfMinutes")]
        public int ExpiresInOfMinutes { get; set; }

        [AppConfigurationBinder("JwtRefreshTokenExpiryTimeOfMinutes")]
        public int RefreshTokenExpiryTimeOfMinutes { get; set; }
    }
}