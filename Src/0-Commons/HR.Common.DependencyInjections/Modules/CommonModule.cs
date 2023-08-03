using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using HR.Common.Attributes.Modules;
using HR.Common.Configurations.Options;

namespace HR.Common.DependencyInjections.Modules
{
    public class CommonModule : Module
    {
        public CommonModule(CommonModuleOptions options)
        {
            Options = options;
        }

        protected CommonModuleOptions Options { get; }

        protected override void Load(ContainerBuilder builder)
        {
            if (Options.Type == CommonModuleOptions.ModuleType.None)
            {
                return;
            }

            var registerBuilder = builder.RegisterAssemblyTypes(Options.Assembly);
            RegisterOtherModuleType(registerBuilder);
        }

        private void RegisterOtherModuleType(IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> registrationBuilder)
        {
            registrationBuilder.Where(w =>
            {
                if (w.CustomAttributes.Any(t => t.AttributeType == typeof(IgnoreAutoDIAttribute)))
                {
                    return false;
                }

                if (Options.Type == CommonModuleOptions.ModuleType.CQRS)
                {
                    return w.Name.EndsWith("Handler");
                }
                else
                {
                    return w.Name.EndsWith(Options.Type.ToString());
                }
            }).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}