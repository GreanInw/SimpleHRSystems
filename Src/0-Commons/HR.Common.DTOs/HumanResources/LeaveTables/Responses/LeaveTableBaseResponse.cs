namespace HR.Common.DTOs.HumanResources.LeaveTables.Responses
{
    public class LeaveTableBaseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Days { get; set; }
        public bool IsActive { get; set; }
        public int LanguageId { get; set; }
    }
}
