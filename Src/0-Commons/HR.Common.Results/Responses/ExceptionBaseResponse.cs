using System.Text.Json.Serialization;

namespace HR.Common.Results.Responses
{
    public class ExceptionBaseResponse
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        [JsonPropertyName("traceId")]
        public string TraceId { get; set; }
    }
}
