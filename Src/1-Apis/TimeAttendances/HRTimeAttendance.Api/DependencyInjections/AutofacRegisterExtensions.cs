using Autofac;
using Autofac.Extensions.DependencyInjection;
using HR.Common.Configurations;
using HR.Common.Configurations.Options;
using HR.Common.Constants;
using HR.Common.DALs;
using HR.Common.DALs.UnitOfWorks;
using HR.Common.DbContexts.Contexts;
using HR.Common.DependencyInjections.Modules;
using HR.Common.Services;
using HRTimeAttendance.Services;

namespace HRTimeAttendance.Api.DependencyInjections
{
    public static class AutofacRegisterExtensions
    {
        public static IHostBuilder RegisterCompomentWithAutofac(this IHostBuilder host, IConfiguration configuration)
        {
            host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterDbContexts(configuration)
                    .RegisterUnitOfWorks()
                    .RegisterCommonModules()
                    .RegisterProviders();
            });
            return host;
        }

        private static ContainerBuilder RegisterDbContexts(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterModule(new DbContextModule<HRDbContext, IHRDbContext>(new DbContextModuleOptions
            {
                SectionName = ConfigurationConstants.ConnectionStrings.HRSystemSectionName
            }));

            return builder;
        }

        private static ContainerBuilder RegisterUnitOfWorks(this ContainerBuilder builder)
        {
            builder.RegisterModule(new UnitOfWorkModule<HRUnitOfWork, IHRUnitOfWork, IHRDbContext>());
            return builder;
        }

        private static ContainerBuilder RegisterCommonModules(this ContainerBuilder builder)
        {
            #region Register common liberies
            builder.RegisterModule(new CommonModule(new CommonModuleOptions
            {
                Assembly = new CommonDALsAssembly().CurrentAssembly,
                Type = CommonModuleOptions.ModuleType.Repository
            }));

            builder.RegisterModule(new CommonModule(new CommonModuleOptions
            {
                Assembly = new CommonServiceAssembly().CurrentAssembly,
                Type = CommonModuleOptions.ModuleType.Service
            }));
            #endregion

            builder.RegisterModule(new CommonModule(new CommonModuleOptions
            {
                Assembly = new ServiceAssembly().CurrentAssembly,
                Type = CommonModuleOptions.ModuleType.Service
            }));

            return builder;
        }

        private static ContainerBuilder RegisterProviders(this ContainerBuilder builder)
        {
            builder.RegisterModule(new ProviderModule(new ProviderModuleOptions
            {
                EnableFormDataFactory = true
            }));

            return builder;
        }
    }
}
