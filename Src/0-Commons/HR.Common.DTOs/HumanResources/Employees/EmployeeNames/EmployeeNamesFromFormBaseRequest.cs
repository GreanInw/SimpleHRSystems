using HR.Common.Libs.Webs.Attributes;

namespace HR.Common.DTOs.HumanResources.Employees.EmployeeNames
{
    public class EmployeeNamesFromFormBaseRequest : IEmployeeNameEntity
    {
        [SnakeCaseFromForm(nameof(FirstName))]
        public string FirstName { get; set; }

        [SnakeCaseFromForm(nameof(LastName))]
        public string LastName { get; set; }
        
        [SnakeCaseFromForm(nameof(MiddleName))]
        public string MiddleName { get; set; }
        
        [SnakeCaseFromForm(nameof(Nickname))]
        public string Nickname { get; set; }
        
        [SnakeCaseFromForm(nameof(LanguageId))]
        public int LanguageId { get; set; }
    }
}
