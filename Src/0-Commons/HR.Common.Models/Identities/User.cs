using HR.Common.Libs.Auditables;
using HR.Common.Models.HumanResources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Common.Models.Identities
{
    [Table(nameof(User))]
    public class User : IAuditableEntity
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(255), Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordSalt { get; set; }
        public bool IsActive { get; set; }

        public DateTime? LatestLogIn { get; set; }
        public DateTime? PasswordExpiredDate { get; set; }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        public virtual UserEmployee UserEmployee { get; set; }
    }
}
