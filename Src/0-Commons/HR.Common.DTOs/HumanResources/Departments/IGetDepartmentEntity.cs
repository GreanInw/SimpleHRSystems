namespace HR.Common.DTOs.HumanResources.Departments
{
    public interface IGetDepartmentEntity
    {
        Guid? ParentId { get; set; }
        int? LanguageId { get; set; }
    }
}
