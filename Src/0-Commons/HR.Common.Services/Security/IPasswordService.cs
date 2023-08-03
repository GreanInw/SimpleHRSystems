using HR.Common.Results;
using HR.Common.Services.Bases;
using static HR.Common.Services.Security.PasswordService;

namespace HR.Common.Services.Security
{
    public interface IPasswordService : IBaseService
    {
        /// <summary>
        /// Validate and ecrypt password
        /// </summary>
        /// <param name="password">Password for encryption</param>
        /// <returns></returns>
        ServiceResult<PasswordResult> EncryptPassword(string password);
        /// <summary>
        /// Validate and ecrypt password
        /// </summary>
        /// <param name="password">Password for encryption</param>
        /// <param name="key">Key for encryption</param>
        /// <returns></returns>
        ServiceResult<PasswordResult> EncryptPassword(string password, string key);
        /// <summary>
        /// Get password salt
        /// </summary>
        /// <returns></returns>
        string GetPasswordSalt();
    }
}
