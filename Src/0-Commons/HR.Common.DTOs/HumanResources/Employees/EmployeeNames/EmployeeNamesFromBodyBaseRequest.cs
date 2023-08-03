using Newtonsoft.Json;

namespace HR.Common.DTOs.HumanResources.Employees.EmployeeNames
{
    [JsonObject]
    public class EmployeeNamesFromBodyBaseRequest : IEmployeeNameEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Nickname { get; set; }
        public int LanguageId { get; set; }
    }
}