using System.Reflection;

namespace HR.Common.Configurations.Options
{
    public class CommonModuleOptions
    {
        public CommonModuleOptions() : this(null, ModuleType.None) { }

        public CommonModuleOptions(Assembly assembly) : this(assembly, ModuleType.None)
        { }

        public CommonModuleOptions(Assembly assembly, ModuleType type)
        {
            Assembly = assembly;
            Type = type;
        }

        public Assembly Assembly { get; set; }
        public ModuleType Type { get; set; }

        public enum ModuleType
        {
            None = -1,
            CQRS,
            Service,
            Repository,
        }
    }
}