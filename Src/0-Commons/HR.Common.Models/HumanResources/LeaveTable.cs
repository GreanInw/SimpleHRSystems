using HR.Common.Libs.Auditables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Common.Models.HumanResources
{
    [Table(nameof(LeaveTable))]
    public class LeaveTable : IAuditableEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public int Days { get; set; }
        public bool IsActive { get; set; }
        public int LanguageId { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
        public virtual ICollection<SummaryLeave> SummaryLeaves { get; set; }
    }
}