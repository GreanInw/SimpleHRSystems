namespace HR.Common.DTOs.Identities.Roles.Responses
{
    public class RoleBaseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
