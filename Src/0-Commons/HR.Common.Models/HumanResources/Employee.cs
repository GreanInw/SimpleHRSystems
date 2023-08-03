using HR.Common.Libs.Auditables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Common.Models.HumanResources
{
    [Table(nameof(Employee))]
    public class Employee : IAuditableEntity
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Internal employee id or employe no.
        /// </summary>
        [MaxLength(50), Required]
        public string InternalId { get; set; }
        public DateTime? Birthday { get; set; }
        [MaxLength(255)]
        public string Nationality { get; set; }
        [MaxLength(250)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string NationalId { get; set; }
        [MaxLength(100)]
        public string ContactNumber { get; set; }

        /// <summary>
        /// Id of <see cref="Department.Id"/> and <see cref="Department.ParentId"/> is null.
        /// </summary>
        public Guid? SectionId { get; set; }

        /// <summary>
        /// Id of <see cref="Department.Id"/> and <see cref="Department.ParentId"/> is not null.
        /// </summary>
        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public virtual UserEmployee UserEmployee { get; set; }

        [ForeignKey(nameof(PositionId))]
        public virtual Position Position { get; set; }

        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
        public virtual ICollection<EmployeeNamesInfo> EmployeeNamesInfos { get; set; }
        public virtual ICollection<EmployeeAddress> EmployeeAddresses { get; set; }
        public virtual ICollection<SummaryLeave> SummaryLeaves { get; set; }
    }
}