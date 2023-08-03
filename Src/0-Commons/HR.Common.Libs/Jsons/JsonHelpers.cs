using HR.Common.Results;
using System.Text.Json.Serialization;

namespace HR.Common.Libs.Jsons
{
    public sealed class JsonHelpers
    {
        /// <summary>
        /// Get default <see cref="System.Text.Json.JsonSerializerOptions"/> for snake case and property enum.
        /// </summary>
        /// <returns></returns>
        public static System.Text.Json.JsonSerializerOptions GetDefaultJsonSerializerOptions()
        {
            var options = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = SnakeCaseJsonNamingPolicy.Instance,
                DictionaryKeyPolicy = SnakeCaseJsonNamingPolicy.Instance
            };
            options.Converters.Add(new JsonStringEnumConverter());
            return options;
        }

        /// <summary>
        /// Get default <see cref="Newtonsoft.Json.JsonSerializerSettings"/> for snake case and property enum.
        /// </summary>
        /// <returns></returns>
        public static Newtonsoft.Json.JsonSerializerSettings GetDefaultJsonSerializerSettings()
        {
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                {
                    NamingStrategy = new Newtonsoft.Json.Serialization.SnakeCaseNamingStrategy
                    {
                        OverrideSpecifiedNames = true,
                        ProcessDictionaryKeys = true
                    }
                }
            };
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            return settings;
        }

        public static ServiceResult DeserializeToServiceResult(string json)
            => System.Text.Json.JsonSerializer.Deserialize<ServiceResult>(json, GetDefaultJsonSerializerOptions());

        public static ServiceResult<TResult> DeserializeToServiceResult<TResult>(string json)
            => System.Text.Json.JsonSerializer.Deserialize<ServiceResult<TResult>>(json, GetDefaultJsonSerializerOptions());

        public static async ValueTask<ServiceResult> DeserializeToServiceResultAsync(HttpResponseMessage response)
            => DeserializeToServiceResult(await response.Content.ReadAsStringAsync());

        public static async ValueTask<ServiceResult<TResult>> DeserializeToServiceResultAsync<TResult>(HttpResponseMessage response)
            => DeserializeToServiceResult<TResult>(await response.Content.ReadAsStringAsync());
    }
}
