using HR.Common.Models.Commons;

namespace HR.Common.Services.AppConfigurations.Extensions
{
    public static class AppConfigurationExtensions
    {
        public static string GetValue(this IEnumerable<AppConfiguration> sources, string name)
            => sources.FirstOrDefault(w => w.Name == name)?.Value;

        public static int GetIntValue(this IEnumerable<AppConfiguration> sources, string name)
            => sources.GetValueByName<int>(name);

        public static bool GetBooleanValue(this IEnumerable<AppConfiguration> sources, string name)
            => sources.GetValueByName<bool>(name);

        private static T GetValueByName<T>(this IEnumerable<AppConfiguration> sources, string name)
            => sources.FirstOrDefault(x => x.Name == name).GetValueByAppConfiguration<T>();

        private static T GetValueByAppConfiguration<T>(this AppConfiguration source)
            => source is null ? default : (T)Convert.ChangeType(source.Value, typeof(T));
    }
}
