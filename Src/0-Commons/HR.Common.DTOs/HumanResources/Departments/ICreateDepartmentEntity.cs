using HR.Common.DTOs.Languages;

namespace HR.Common.DTOs.HumanResources.Departments
{
    public interface ICreateDepartmentEntity : ILanguageIdEntity
    {
        string Name { get; set; }
        Guid? ParentId { get; set; }
    }
}