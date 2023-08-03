using HR.Common.Configurations.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace HR.Common.Libs.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder IncludeOtherSettingFiles(this IHostBuilder builder, IncludeOtherSettingOptions options = null)
            => builder.ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                config.AddJsonFile("swaggersettings.json", false);

                if (options is not null)
                {
                    string envName = context.HostingEnvironment.IsLocal()
                        ? "" : $".{context.HostingEnvironment.EnvironmentName}";
                    if (options.IncludeUrlSettings)
                    {
                        config.AddJsonFile($"urlsettings{envName}.json", false);

                    }
                }
                config.AddEnvironmentVariables();
            });
    }
}
