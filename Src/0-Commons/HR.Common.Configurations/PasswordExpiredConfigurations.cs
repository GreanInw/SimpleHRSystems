namespace HR.Common.Configurations
{
    public class PasswordExpiredConfigurations
    {
        public bool EnablePasswordExpired { get; set; }
        public bool EnableForceChangePassword { get; set; }
        public int DaysOfPasswordExpired { get; set; }
    }
}
