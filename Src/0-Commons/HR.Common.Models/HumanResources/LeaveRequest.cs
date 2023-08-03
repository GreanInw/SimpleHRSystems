using HR.Common.Enums;
using HR.Common.Libs.Auditables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Common.Models.HumanResources
{
    [Table(nameof(LeaveRequest))]
    public class LeaveRequest : IAuditableEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid LeaveId { get; set; }
        public Guid EmployeeId { get; set; }
        [MaxLength(500)]
        public string Remarks { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Days { get; set; }

        [Required, MaxLength(50)]
        public LeaveRequestStatus Status { get; set; }

        [MaxLength(255)]
        public string ActionBy { get; set; }
        public DateTime? ActionDate { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        [ForeignKey(nameof(LeaveId))]
        public virtual LeaveTable LeaveTable { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee Employee { get; set; }
    }
}