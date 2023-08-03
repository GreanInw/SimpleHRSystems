namespace HR.Common.Libs.Auditables
{
    public interface IAuditableEntity : ICreatedAuditableEntity
    {
        // <summary>
        /// Modified date
        /// </summary>
        DateTime ModifiedDate { get; set; }
        /// <summary>
        /// Modified by
        /// </summary>
        string ModifiedBy { get; set; }
    }
}
