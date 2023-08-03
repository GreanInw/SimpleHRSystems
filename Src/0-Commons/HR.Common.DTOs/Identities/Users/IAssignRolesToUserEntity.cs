namespace HR.Common.DTOs.Identities.Users
{
    public interface IAssignRolesToUserEntity
    {
        Guid UserId { get; set; }
        IEnumerable<Guid> RoleIds { get; set; }
    }
}
