using HR.Common.DTOs.Languages;

namespace HR.Common.DTOs.HumanResources.Holidays
{
    public interface ICreateHolidayEntity : ILanguageIdEntity
    {
        DateTime Date { get; set; }
        string Name { get; set; }
    }
}
