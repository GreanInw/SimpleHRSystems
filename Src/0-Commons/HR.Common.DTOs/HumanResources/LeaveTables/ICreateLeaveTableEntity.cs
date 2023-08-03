using HR.Common.DTOs.Languages;

namespace HR.Common.DTOs.HumanResources.LeaveTables
{
    public interface ICreateLeaveTableEntity : ILanguageIdEntity
    {
        string Name { get; set; }
        int Days { get; set; }
    }
}