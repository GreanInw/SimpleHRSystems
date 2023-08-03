using Microsoft.Extensions.Configuration;

namespace HR.Common.Libs.Extensions
{
    public static class CommonConfigurationExtensions
    {
        public static T GetValueBySection<T>(this IConfiguration configuration, string key)
            where T : class, new()
        {
            var value = new T();    
            configuration.GetSection(key).Bind(value);
            return value;
        }
    }
}
