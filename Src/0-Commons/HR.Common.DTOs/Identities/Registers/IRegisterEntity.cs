namespace HR.Common.DTOs.Identities.Registers
{
    public interface IRegisterEntity
    {
        string Username { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
    }
}
