namespace HR.Common.Attributes.Modules
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class IgnoreAutoDIAttribute : Attribute
    {
        public IgnoreAutoDIAttribute()
        { }
    }
}
