using System.Text.Json.Serialization;

namespace HR.Common.DTOs.Bases
{
    public class PagingFromBodyBase : PagingBase
    {
        [JsonPropertyName("limit")]
        public override int Limit { get => base.Limit; set => base.Limit = value; }
        [JsonPropertyName("page_number")]
        public override int PageNumber { get => base.PageNumber; set => base.PageNumber = value; }
    }
}