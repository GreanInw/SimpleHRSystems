using HR.Common.Libs.Auditables;
using System.ComponentModel.DataAnnotations;

namespace HR.Common.Models.HumanResources
{
    public class Holiday : IAuditableEntity
    {
        [Key]
        public Guid Id { get; set; }
        public int LanguageId { get; set; }
        public DateTime Date { get; set; }
        [Required, MaxLength(255)]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
