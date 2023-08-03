using HR.Common.Configurations;
using HR.Common.Constants;
using HR.Common.DALs.Repositories.Identities.SSO.Commands;
using HR.Common.DALs.UnitOfWorks;
using HR.Common.DTOs.Identities.Users;
using HR.Common.Models.Identities;
using HR.Common.Results;
using HR.Common.Services.Security;
using Identities.Services.Users.Bases;
using Microsoft.AspNetCore.Http;
using static HR.Common.Services.Security.PasswordService;

namespace Identities.Services.Passwords.Commands
{
    public class UserPasswordCommandService : UserCommandBaseService
        , IUserPasswordCommandService
    {
        public UserPasswordCommandService(IHttpContextAccessor httpContextAccessor
            , IHRUnitOfWork unitOfWork, IUserCommandRepository userCommandRepository
            , IPasswordService passwordService)
            : base(httpContextAccessor, unitOfWork, userCommandRepository)
        {
            PasswordService = passwordService;
            PasswordExpiredConfigs = BindAppConfigurationsByNamesInternal<PasswordExpiredConfigurations>();
        }

        protected PasswordExpiredConfigurations PasswordExpiredConfigs { get; }
        protected IPasswordService PasswordService { get; }

        public async ValueTask<ServiceResult> ChangePasswordAsync(IChangePasswordEntity entity)
        {
            var user = await GetUserAsync(entity.Username);
            if (user is null)
            {
                return new ServiceResult(ErrorMessageConstants.Identities.Users.UserNotFound);
            }

            var oldPaswordResult = PasswordService.EncryptPassword(entity.OldPassword, user.PasswordSalt);
            if (oldPaswordResult.Result.Password != user.Password)
            {
                return new ServiceResult(ErrorMessageConstants.Identities.Users.OldPasswordInvalid);
            }

            var newPasswordResult = PasswordService.EncryptPassword(entity.NewPassword);
            if (!newPasswordResult.IsSuccess)
            {
                return new ServiceResult(newPasswordResult.Errors);
            }

            SetPasswordInfoOfUser(user, newPasswordResult.Result);
            UserCommandRepository.Edit(user);
            await UnitOfWork.CommitAsync();

            return new ServiceResult(true);
        }

        public async ValueTask<ServiceResult> ResetPasswordAsync(IResetPasswordEntity entity)
        {
            var user = await GetUserAsync(entity.Username);
            if (user is null)
            {
                return new ServiceResult(ErrorMessageConstants.Identities.Users.UserNotFound);
            }

            var newPasswordResult = PasswordService.EncryptPassword(entity.NewPassword);
            if (!newPasswordResult.IsSuccess)
            {
                return new ServiceResult(newPasswordResult.Errors);
            }

            SetPasswordInfoOfUser(user, newPasswordResult.Result);
            UserCommandRepository.Edit(user);
            await UnitOfWork.CommitAsync();

            return new ServiceResult(true);
        }

        protected void SetPasswordInfoOfUser(User user, PasswordResult passwordResult)
        {
            user.Password = passwordResult.Password;
            user.PasswordSalt = passwordResult.Salt;
            if (PasswordExpiredConfigs.EnablePasswordExpired && PasswordExpiredConfigs.DaysOfPasswordExpired > 0)
            {
                user.PasswordExpiredDate = DateTime.UtcNow
                    .AddDays(PasswordExpiredConfigs.DaysOfPasswordExpired);
            }
        }
    }
}
