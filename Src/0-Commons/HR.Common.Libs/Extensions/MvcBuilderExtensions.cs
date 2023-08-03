using HR.Common.Libs.Jsons;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace HR.Common.Libs.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddSnakeCaseJsonResponse(this IMvcBuilder builder)
        {
            builder.AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DictionaryKeyPolicy = SnakeCaseJsonNamingPolicy.Instance;
                options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseJsonNamingPolicy.Instance;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            return builder;
        }
    }
}
