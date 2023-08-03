using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace HR.Common.Libs.Jsons
{
    public class SnakeCaseJsonNamingPolicy : JsonNamingPolicy
    {
        private readonly SnakeCaseNamingStrategy _snakeCaseNamingStrategy;

        public SnakeCaseJsonNamingPolicy()
        {
            _snakeCaseNamingStrategy = new SnakeCaseNamingStrategy();
        }

        public static SnakeCaseJsonNamingPolicy Instance => new SnakeCaseJsonNamingPolicy();

        public override string ConvertName(string name)
            => _snakeCaseNamingStrategy.GetPropertyName(name, false);
    }
}