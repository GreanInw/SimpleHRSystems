using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace HR.Common.DependencyInjections.Extensions
{
    public static class ConfigurationApplicationExtensions
    {
        public static void CommonConfigures(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/v1.0/error");
                app.UseHsts();
            }
            else
            {
                app.UseExceptionHandler("/v1.0/error-dev");
            }
        }
    }
}
