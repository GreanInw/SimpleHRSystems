using HR.Common.Libs.Auditables;

namespace HR.Common.Models.HumanResources
{
    public class SummaryLeave : IAuditableEntity
    {
        public Guid EmployeeId { get; set; }
        public Guid LeaveId { get; set; }
        public int TotalLeaveDays { get; set; }
        public int RemainLeaveDays { get; set; }
        public int UseLeaveDays { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public Employee Employee { get; set; }
        public LeaveTable LeaveTable { get; set; }
    }
}