namespace HR.Common.DTOs.Identities.Users
{
    public interface IResetPasswordEntity
    {
        public string Username { get; set; }
        public string NewPassword { get; set; }
    }
}
