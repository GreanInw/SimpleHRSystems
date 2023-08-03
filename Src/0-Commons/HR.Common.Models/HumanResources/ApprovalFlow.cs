using HR.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Common.Models.HumanResources
{
    [Table(nameof(ApprovalFlow))]
    public class ApprovalFlow
    {
        /// <summary>
        /// Id ของคนที่มีสิทธิ Approve.
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// Id ของคนสร้างเอกสาร Request  
        /// </summary>
        public Guid ChildId { get; set; }

        /// <summary>
        /// Type of <see cref="ApprovalFlowType"/>
        /// </summary>
        [MaxLength(50), Required]
        public ApprovalFlowType Type { get; set; }
        public int Sequence { get; set; }
    }
}