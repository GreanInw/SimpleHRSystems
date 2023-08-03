namespace HR.Common.DTOs.Identities.SignIns.Bases
{
    public interface ISignInEntity
    {
        string Username { get; set; }
        string Password { get; set; }
    }
}