namespace HR.Common.Identities.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class AllowRoleAuthorizeAttribute : Attribute { }
}
