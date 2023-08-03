namespace HR.Common.Configurations
{
    public class HRUrlSettings
    {
        public IdentityApiSettings IdentityApi { get; set; }

        public class IdentityApiSettings
        {
            public string Host { get; set; }
            public string RegisterUrl { get; set; }
        }
    }
}
