using Autofac;
using HR.Common.Configurations.Options;
using HR.Common.Libs.Providers.FormDatas;

namespace HR.Common.DependencyInjections.Modules
{
    public class ProviderModule : Module
    {
        public ProviderModule(ProviderModuleOptions options)
        {
            Options = options;
        }

        public ProviderModuleOptions Options { get; }

        protected override void Load(ContainerBuilder builder)
        {
            if (Options is null)
            {
                return;
            }

            if (Options.EnableFormDataFactory)
            {
                builder.RegisterType<FormDataFactory>().As<IFormDataFactory>()
                    .InstancePerLifetimeScope();
            }
        }
    }
}
