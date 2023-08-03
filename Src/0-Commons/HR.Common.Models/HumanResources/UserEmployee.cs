using HR.Common.Models.Identities;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Common.Models.HumanResources
{
    [Table(nameof(UserEmployee))]
    public class UserEmployee
    {
        public Guid UserId { get; set; }
        public Guid EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee Employee { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}