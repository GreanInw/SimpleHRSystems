using HR.Common.DTOs.HumanResources.Employees.EmployeeNames;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HR.Common.DTOs.HumanResources.Employees.Requests
{
    [JsonObject]
    public class EmployeeFromBodyBaseRequest : IEmployeeEntity<EmployeeNamesFromBodyBaseRequest>
    {
        [Required, DisplayName("internal id")]
        public string InternalId { get; set; }
        public DateTime? Birthday { get; set; }
        public string Nationality { get; set; }
        public string Email { get; set; }
        public string NationalId { get; set; }
        public string ContactNumber { get; set; }
        public Guid? SectionId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }
        public IList<EmployeeNamesFromBodyBaseRequest> Names { get; set; }
    }
}