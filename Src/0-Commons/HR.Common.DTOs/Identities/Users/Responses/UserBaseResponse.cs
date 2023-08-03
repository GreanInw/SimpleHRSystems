namespace HR.Common.DTOs.Identities.Users.Responses
{
    public class UserBaseResponse
    {
        public UserBaseResponse() => Roles = new List<RoleResponse>();

        public Guid UserId { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LatestLogIn { get; set; }
        public IEnumerable<RoleResponse> Roles { get; set; }

        public class RoleResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }
}