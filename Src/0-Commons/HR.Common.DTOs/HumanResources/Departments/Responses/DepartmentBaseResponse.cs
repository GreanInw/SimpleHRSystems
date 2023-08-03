using Newtonsoft.Json;

namespace HR.Common.DTOs.HumanResources.Departments.Responses
{
    [JsonObject]
    public class DepartmentBaseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsActive { get; set; }
        public int LanguageId { get; set; }
    }
}
