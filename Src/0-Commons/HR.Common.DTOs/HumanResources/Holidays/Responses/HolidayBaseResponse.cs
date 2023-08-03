using HR.Common.DTOs.Bases;
using HR.Common.DTOs.Languages;
using Newtonsoft.Json;

namespace HR.Common.DTOs.HumanResources.Holidays.Responses
{
    [JsonObject]
    public class HolidayBaseResponse : IIdBaseEntity, ILanguageIdEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public int LanguageId { get; set; }
    }
}
