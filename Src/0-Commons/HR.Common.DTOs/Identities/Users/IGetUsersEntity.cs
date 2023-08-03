namespace HR.Common.DTOs.Identities.Users
{
    public interface IGetUsersEntity
    {
        string Username { get; set; }
        bool? IsActive { get; set; }
    }
}
