using HR.Common.DTOs.Languages;

namespace HR.Common.DTOs.HumanResources.Employees.EmployeeNames
{
    public interface IEmployeeNameEntity : ILanguageIdEntity
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string MiddleName { get; set; }
        string Nickname { get; set; }
    }
}