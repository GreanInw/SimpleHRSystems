using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Common.Models.Identities
{
    [Table(nameof(UserInRole))]
    public class UserInRole
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual Role Role { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
