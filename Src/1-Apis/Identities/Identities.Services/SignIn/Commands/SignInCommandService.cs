using HR.Common.Configurations;
using HR.Common.Constants;
using HR.Common.DALs.Repositories.Identities.SSO.Commands;
using HR.Common.DALs.UnitOfWorks;
using HR.Common.DTOs.Identities.SignIns.Bases;
using HR.Common.DTOs.Identities.SignIns.Responses;
using HR.Common.Identities.Jwt;
using HR.Common.Libs.Extensions;
using HR.Common.Models.Commons;
using HR.Common.Models.Identities;
using HR.Common.Results;
using HR.Common.Services.AppConfigurations.Helpers;
using HR.Common.Services.Bases;
using HR.Common.Services.Security;
using Identities.Services.SignIn.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Identities.Users.Services.SignIn.Commands
{
    public class SignInCommandService : BaseCommonCommandService<IHRUnitOfWork>, ISignInCommandService
    {
        public SignInCommandService(IHttpContextAccessor httpContextAccessor, IHRUnitOfWork unitOfWork
            , IPasswordService passwordService, IUserCommandRepository userCommandRepository)
            : base(httpContextAccessor, unitOfWork)
        {
            PasswordService = passwordService;
            UserCommandRepository = userCommandRepository;

            InitialData();
        }

        protected IPasswordService PasswordService { get; }
        protected IUserCommandRepository UserCommandRepository { get; }

        protected IEnumerable<AppConfiguration> AppConfigurations { get; private set; }
        protected JwtConfigurations JwtConfiguration { get; private set; }
        protected PasswordExpiredConfigurations PasswordExpiredConfigs { get; private set; }
        protected IdentitySettings IdentitySettings { get; private set; }

        public async ValueTask<ServiceResult<T>> SignInAsync<T>(ISignInEntity entity)
            where T : SignInBaseResponse, new()
        {
            var user = await GetUser(entity.Username);
            if (user is null)
            {
                return new ServiceResult<T>(ErrorMessageConstants.Identities.Users.InvalidUsernameOrPassword);
            }

            if (!user.IsActive)
            {
                return new ServiceResult<T>(ErrorMessageConstants.Identities.Users.UserInactive);
            }

            var passwordResult = PasswordService.EncryptPassword(entity.Password, user.PasswordSalt);
            if (!passwordResult.IsSuccess)
            {
                return new ServiceResult<T>(ErrorMessageConstants.Identities.Users.InvalidUsernameOrPassword);
            }

            bool valid = passwordResult.Result.Password == user.Password;
            if (!valid)
            {
                return new ServiceResult<T>(ErrorMessageConstants.Identities.Users.InvalidUsernameOrPassword);
            }

            var token = GenerateJwtToken<T>(user);
            await UpdateUserData(user, token);
            SetForceChangePassword(user, token);

            return new ServiceResult<T>(true, token);
        }

        public async ValueTask<ServiceResult<T>> RefreshTokenAsync<T>(IRefreshTokenEntity entity)
            where T : SignInBaseResponse, new()
        {
            var user = await GetUser(entity: entity);
            if (user is null)
            {
                return new ServiceResult<T>("Access token or Refresh token invalid.");
            }

            if (!user.IsActive)
            {
                return new ServiceResult<T>(ErrorMessageConstants.Identities.Users.UserInactive);
            }

            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return new ServiceResult<T>("Refresh token expired.");
            }

            var token = GenerateJwtToken<T>(user);
            await UpdateUserData(user, token);
            return new ServiceResult<T>(true, token);
        }

        protected void InitialData()
        {
            var configurationNames = AppConfigurationBinderHelper
                .GetConfigurationNames(new List<Type> { typeof(JwtConfigurations), typeof(PasswordExpiredConfigurations) })
                .ToArray();

            AppConfigurations = GetAppConfigurationsByNamesInternalAsync(configurationNames)
                .ConfigureAwait(false).GetAwaiter().GetResult();

            IdentitySettings = Configuration.GetValueBySection<IdentitySettings>(nameof(IdentitySettings));
            JwtConfiguration = BindConfigurations<JwtConfigurations>(AppConfigurations);
            PasswordExpiredConfigs = BindConfigurations<PasswordExpiredConfigurations>(AppConfigurations);
        }

        private async Task UpdateUserData(User user, SignInBaseResponse response)
        {
            user.LatestLogIn = DateTime.UtcNow;
            user.AccessToken = response.AccessToken;
            user.RefreshToken = response.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow
                .AddMinutes(JwtConfiguration.RefreshTokenExpiryTimeOfMinutes);

            UserCommandRepository.Edit(user);
            await UnitOfWork.CommitAsync();
        }

        private async ValueTask<User> GetUser(string username = "", IRefreshTokenEntity entity = null)
        {
            if (username.IsEmpty() && entity is null)
            {
                throw new Exception($"Cannot get user data, Because argument '{nameof(username)}' or '{nameof(entity)}' null");
            }

            Expression<Func<User, bool>> expression = filter => true;

            if (!username.IsEmpty())
            {
                expression = expression.And(w => w.Username.ToLower() == username.ToLower());
            }
            else
            {
                expression = expression.And(w => w.AccessToken == entity.AccessToken
                    && w.RefreshToken == entity.RefreshToken);
            }

            return await UserCommandRepository.All
                .Include(t => t.UserInRoles).ThenInclude(t => t.Role)
                .Include(t => t.UserEmployee)
                .FirstOrDefaultAsync(expression);
        }

        private T GenerateJwtToken<T>(User user) where T : SignInBaseResponse, new()
        {
            var identityToken = new IdentityTokenHelper(JwtConfiguration, IdentitySettings);
            var claims = new List<Claim>
            {
                new Claim(JwtIdentityConstants.ClaimNames.OId, user.Id.ToString()),
                new Claim(JwtIdentityConstants.ClaimNames.Sub, user.Username),
                new Claim(JwtIdentityConstants.ClaimNames.Email, user.Username)
            };

            if (user.UserInRoles is not null)
            {
                //Add roles
                foreach (var role in user.UserInRoles)
                {
                    claims.Add(new Claim(JwtIdentityConstants.ClaimNames.Role, role.Role.Name));
                }
            }

            if (user.UserEmployee is not null)
            {
                claims.Add(new Claim(JwtIdentityConstants.ClaimNames.EmployeeId
                    , user.UserEmployee.EmployeeId.ToString()));
            }

            return new T
            {
                AccessToken = identityToken.GenerateToken(claims),
                RefreshToken = identityToken.GenerateRefreshToken()
            };
        }

        private void SetForceChangePassword(User user, SignInBaseResponse response)
        {
            if (!PasswordExpiredConfigs.EnablePasswordExpired
                || !user.PasswordExpiredDate.HasValue)
            {
                return;
            }

            bool isExpire = user.PasswordExpiredDate.Value > DateTime.UtcNow.AddDays(PasswordExpiredConfigs.DaysOfPasswordExpired);
            response.EnableForceChangePassword = isExpire && PasswordExpiredConfigs.EnableForceChangePassword;
        }
    }
}