using HR.Common.Models.Commons;
using HR.Common.Services.AppConfigurations.Extensions;
using HR.Common.Services.AppConfigurations.Helpers.Attributes;
using System.CodeDom;
using System.Reflection;

namespace HR.Common.Services.AppConfigurations.Helpers
{
    public class AppConfigurationBinderHelper
    {
        public static TConfigs Bind<TConfigs>(IEnumerable<AppConfiguration> configurations)
            where TConfigs : class, new()
        {
            var configValue = new TConfigs();
            foreach (var property in typeof(TConfigs).GetProperties())
            {
                var binderAttr = property.GetCustomAttribute<AppConfigurationBinderAttribute>();
                string name = binderAttr is null ? property.Name : binderAttr.Name;
                var valueOfConfig = GetValueByAppConfiguration(property.PropertyType, name, configurations);
                property.SetValue(configValue, valueOfConfig);
            }

            return configValue;
        }

        public static IEnumerable<string> GetConfigurationNames<TConfigs>() where TConfigs : class, new()
            => GetConfigurationNames(typeof(TConfigs));

        public static IEnumerable<string> GetConfigurationNames(IEnumerable<Type> types)
        {
            var names = new List<string>();
            foreach (var item in types)
            {
                names.AddRange(GetConfigurationNames(item));
            }
            return names;
        }

        public static IEnumerable<string> GetConfigurationNames(Type type)
            => type.GetProperties().Select(s =>
            {
                var binderAttr = s.GetCustomAttribute<AppConfigurationBinderAttribute>();
                return binderAttr is null ? s.Name : binderAttr.Name;
            });

        internal static object GetValueByAppConfiguration(Type originalType, string name
            , IEnumerable<AppConfiguration> appConfigurations)
        {
            if (originalType == typeof(int))
            {
                return appConfigurations.GetIntValue(name);
            }
            else if (originalType == typeof(bool))
            {
                return appConfigurations.GetBooleanValue(name);
            }
            else
            {
                return appConfigurations.GetValue(name);
            }
        }
    }
}