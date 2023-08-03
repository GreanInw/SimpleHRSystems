namespace HR.Common.DTOs.Identities.Users
{
    public interface IChangePasswordEntity
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}