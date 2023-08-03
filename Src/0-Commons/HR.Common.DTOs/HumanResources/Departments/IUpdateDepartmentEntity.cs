using HR.Common.DTOs.Bases;

namespace HR.Common.DTOs.HumanResources.Departments
{
    public interface IUpdateDepartmentEntity : ICreateDepartmentEntity, IIdBaseEntity
    {
        bool IsActive { get; set; }
    }
}