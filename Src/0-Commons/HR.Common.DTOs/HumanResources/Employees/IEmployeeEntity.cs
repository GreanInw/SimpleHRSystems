using HR.Common.DTOs.HumanResources.Employees.EmployeeNames;

namespace HR.Common.DTOs.HumanResources.Employees
{
    public interface IEmployeeEntity<TEmployeeNames> where TEmployeeNames : IEmployeeNameEntity
    {
        string InternalId { get; set; }
        DateTime? Birthday { get; set; }
        string Nationality { get; set; }
        string Email { get; set; }
        string NationalId { get; set; }
        string ContactNumber { get; set; }
        Guid? SectionId { get; set; }
        Guid? DepartmentId { get; set; }
        Guid? PositionId { get; set; }

        IList<TEmployeeNames> Names { get; set; }
    }
}