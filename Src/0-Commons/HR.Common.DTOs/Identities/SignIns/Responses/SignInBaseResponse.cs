namespace HR.Common.DTOs.Identities.SignIns.Responses
{
    public class SignInBaseResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool EnableForceChangePassword { get; set; }
    }
}
