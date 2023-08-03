using HR.Common.Libs.Auditables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Common.Models.Identities
{
    [Table(nameof(Role))]
    public class Role : IAuditableEntity
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(100), Required]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<UserInRole> UserInRoles { get; set; }
    }
}
