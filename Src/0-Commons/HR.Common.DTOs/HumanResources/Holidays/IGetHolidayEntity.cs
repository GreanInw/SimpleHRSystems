namespace HR.Common.DTOs.HumanResources.Holidays
{
    public interface IGetHolidayEntity
    {
        int? Year { get; set; }
        int? LanguageId { get; set; }
    }
}
