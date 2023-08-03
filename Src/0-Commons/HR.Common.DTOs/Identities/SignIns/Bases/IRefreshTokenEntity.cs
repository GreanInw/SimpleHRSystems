namespace HR.Common.DTOs.Identities.SignIns.Bases
{
    public interface IRefreshTokenEntity
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
