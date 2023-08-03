namespace HR.Common.Libs.Auditables
{
    public interface ICreatedAuditableEntity
    {
        /// <summary>
        /// Created date
        /// </summary>
        DateTime CreatedDate { get; set; }
        /// <summary>
        /// Create by
        /// </summary>
        string CreatedBy { get; set; }
    }
}