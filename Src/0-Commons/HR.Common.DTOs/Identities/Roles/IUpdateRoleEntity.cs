namespace HR.Common.DTOs.Identities.Roles
{
    public interface IUpdateRoleEntity
    {
        Guid Id { get; set; }
        string Description { get; set; }
        bool IsActive { get; set; }
    }
}
