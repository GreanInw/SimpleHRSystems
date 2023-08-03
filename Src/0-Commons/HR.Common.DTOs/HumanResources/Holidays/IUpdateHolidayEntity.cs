namespace HR.Common.DTOs.HumanResources.Holidays
{
    public interface IUpdateHolidayEntity : ICreateHolidayEntity
    {
        Guid Id { get; set; }
        bool IsActive { get; set; }
    }
}
