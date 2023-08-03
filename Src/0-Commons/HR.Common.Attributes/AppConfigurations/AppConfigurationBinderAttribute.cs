namespace HR.Common.Services.AppConfigurations.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AppConfigurationBinderAttribute : Attribute
    {
        public AppConfigurationBinderAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
