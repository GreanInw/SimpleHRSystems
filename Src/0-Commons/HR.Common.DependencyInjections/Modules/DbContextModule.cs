using Autofac;
using HR.Common.Configurations.Options;
using HR.Common.EFCores.DbContexts;
using HR.Common.EFCores.DbOptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HR.Common.DependencyInjections.Modules
{
    public class DbContextModule<TDbContext, TInterfaceDbContext> : Module
        where TDbContext : DbContext
        where TInterfaceDbContext : IDbContext
    {
        public DbContextModule(DbContextModuleOptions options)
        {
            Options = options;
        }

        protected DbContextModuleOptions Options { get; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(com =>
            {
                var configuration = com.Resolve<IConfiguration>();
                string connectionString = configuration.GetValue<string>(Options.SectionName);
                return DbOptionBuilders.CreateDbContextOptions<TDbContext>(connectionString);
            }).SingleInstance();

            builder.Register(com =>
            {
                var dbContextOptions = com.Resolve<DbContextOptions<TDbContext>>();
                return (TDbContext)Activator.CreateInstance(typeof(TDbContext), dbContextOptions);
            }).As<TInterfaceDbContext>().AsSelf().InstancePerLifetimeScope();
        }
    }
}