using HR.Common.Services.AppConfigurations.Helpers.Attributes;

namespace HR.Common.Configurations
{
    /// <summary>
    /// Password configurations
    /// </summary>
    public class PasswordConfigurations
    {
        /// <summary>
        /// Minimum length of password
        /// </summary>
        [AppConfigurationBinder("PasswordMinimumLength")]
        public int MinimumLength { get; set; }
        /// <summary>
        /// Maximum length of password
        /// </summary>
        [AppConfigurationBinder("PasswordMaximumLength")]
        public int MaximumLength { get; set; }
    }
}