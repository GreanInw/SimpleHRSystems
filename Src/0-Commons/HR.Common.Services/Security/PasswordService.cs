using HR.Common.Configurations;
using HR.Common.Identities;
using HR.Common.Identities.Security;
using HR.Common.Libs.Extensions;
using HR.Common.Results;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;

namespace HR.Common.Services.Security
{
    /// <summary>
    /// Password service
    /// </summary>
    public class PasswordService : BaseCommonService, IPasswordService
    {
        public PasswordService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            InitialData();
        }

        protected PasswordConfigurations PasswordConfigs { get; private set; }

        public ServiceResult<PasswordResult> EncryptPassword(string password, string key)
            => EncryptPasswordInternal(password, key);

        public ServiceResult<PasswordResult> EncryptPassword(string password)
            => EncryptPasswordInternal(password);

        public string GetPasswordSalt()
            => new HMACSHA512Cryption(Guid.NewGuid().ToString()).Encrypt();

        protected ServiceResult<PasswordResult> EncryptPasswordInternal(string password, string key = "")
        {
            var passwordHelper = new PasswordHelper(PasswordConfigs.MinimumLength, PasswordConfigs.MaximumLength);
            var valid = passwordHelper.ValidatePassword(password);
            string salt = key.IsEmpty() ? GetPasswordSalt() : key;

            return new ServiceResult<PasswordResult>
            {
                IsSuccess = valid,
                Result = valid ? new PasswordResult
                {
                    Password = passwordHelper.Encrypt(password, salt),
                    Salt = salt
                } : null,
                Errors = valid ? null : passwordHelper.GetInvalidMessage(false)
            };
        }

        protected void InitialData()
        {
            PasswordConfigs = BindAppConfigurationsByNamesInternalAsync<PasswordConfigurations>()
                .ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public class PasswordResult
        {
            public string Password { get; set; }
            public string Salt { get; set; }
        }
    }
}