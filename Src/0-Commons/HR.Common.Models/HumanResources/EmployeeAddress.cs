using HR.Common.Enums;
using HR.Common.Libs.Auditables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Common.Models.HumanResources
{
    [Table(nameof(EmployeeAddress))]
    public class EmployeeAddress : IAuditableEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }

        [Required, MaxLength(50)]
        public AddressType Type { get; set; }

        [MaxLength(255)]
        public string Address1 { get; set; }
        [MaxLength(255)]
        public string Address2 { get; set; }
        public int LanguageId { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee Employee { get; set; }
    }
}