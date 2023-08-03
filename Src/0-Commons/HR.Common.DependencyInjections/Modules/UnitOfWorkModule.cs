using Autofac;
using HR.Common.DALs.UnitOfWorks.Bases;
using HR.Common.EFCores.DbContexts;

namespace HR.Common.DependencyInjections.Modules
{
    public class UnitOfWorkModule<TUnitOfWork, TInterfaceUnitOfWork, TInterfaceDbContext> : Module
        where TInterfaceDbContext : IDbContext
        where TUnitOfWork : UnitOfWork<TInterfaceDbContext>
        where TInterfaceUnitOfWork : IUnitOfWork
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(com =>
            {
                var dbContext = com.Resolve<TInterfaceDbContext>();
                return (TUnitOfWork)Activator.CreateInstance(typeof(TUnitOfWork), dbContext);
            }).As<TInterfaceUnitOfWork>().AsSelf()
            .InstancePerLifetimeScope();
        }
    }
}