using System.Reflection;

namespace HR.Common.Libs.Helpers
{
    public abstract class AssemblyHelper
    {
        protected AssemblyHelper(Type type)
        {
            Type = type;
        }

        public Assembly CurrentAssembly => Type?.Assembly;
        public string AssemblyName => CurrentAssembly.GetName().Name;
        protected Type Type { get; }
    }
}