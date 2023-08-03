using HR.Common.DbContexts.Contexts;
using HR.Common.DependencyInjections.Extensions;
using HR.Common.EFCores.Extensions;
using HR.Common.Libs.Extensions;
using HR.Common.Libs.Swaggers;
using HR.Common.Services.BackgroundServices;
using HRTimeAttendance.CQRS;
using HRTimeAttendance.DTOs;

namespace HRTimeAttendance.Api.DependencyInjections
{
    public static class MicorsoftRegisterExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddOptions();
            services.AddLogging();
            services.AddApiVersioning();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CQRSAssembly>());
            services.AddSwaggerInternal(new[]
            {
                new DTOAssembly().CurrentAssembly, new ApiAssembly().CurrentAssembly
            });

            services.RegisterExpcetionFilter();
            services.AddHealthChecks()
                .AddReleaseVersionHealthCheck()
                .AddDbContextHealthCheck(new[] { typeof(IHRDbContext) });

            services.AddControllers().AddSnakeCaseJsonResponse();
            services.AddCustomAuthentication(configuration);
            services.AddHostedService<AutoMigrationService>();
            return services;
        }

        public static void ConfigurationApplication(this WebApplication app)
        {
            app.CommonConfigures();
            app.UseStaticFiles();
            app.UseSwaggerInternal();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHealthChecks();
            app.MapControllers();
        }
    }
}