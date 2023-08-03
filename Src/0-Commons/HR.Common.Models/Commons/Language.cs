using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Common.Models.Commons
{
    [Table(nameof(Language))]
    public class Language
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(2)]
        public string Code { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
    }
}
