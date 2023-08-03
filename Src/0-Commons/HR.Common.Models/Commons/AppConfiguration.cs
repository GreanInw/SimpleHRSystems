using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Common.Models.Commons
{
    [Table(nameof(AppConfiguration))]
    public class AppConfiguration
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }
        
        [Required, MaxLength(100)]
        public string Value { get; set; }
        
        [MaxLength(500)]
        public string Descriptions { get; set; }
        public bool IsActive { get; set; }
    }
}