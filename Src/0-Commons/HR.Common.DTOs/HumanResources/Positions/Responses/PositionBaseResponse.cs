using HR.Common.DTOs.Bases;
using HR.Common.DTOs.Languages;
using Newtonsoft.Json;

namespace HR.Common.DTOs.HumanResources.Positions.Responses
{
    [JsonObject]
    public class PositionBaseResponse : IIdBaseEntity, ILanguageIdEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int LanguageId { get; set; }
    }
}
