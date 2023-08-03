using HR.Common.Configurations;
using HR.Common.DALs.Repositories.Identities.SSO.Commands;
using HR.Common.DALs.UnitOfWorks;
using HR.Common.DTOs.Identities.Registers;
using HR.Common.Models.Identities;
using HR.Common.Results;
using HR.Common.Services.Bases;
using HR.Common.Services.Security;
using Microsoft.AspNetCore.Http;

namespace Identities.Services.Register.Commands
{
    public class RegisterCommandService : BaseCommonCommandService<IHRUnitOfWork>, IRegisterCommandService
    {
        public RegisterCommandService(IHttpContextAccessor httpContextAccessor, IHRUnitOfWork unitOfWork
            , IPasswordService passwordService, IUserCommandRepository userCommandRepository)
            : base(httpContextAccessor, unitOfWork)
        {
            PasswordService = passwordService;
            UserCommandRepository = userCommandRepository;
            PasswordExpiredConfigs = BindAppConfigurationsByNamesInternal<PasswordExpiredConfigurations>();
        }

        protected PasswordExpiredConfigurations PasswordExpiredConfigs { get; }
        protected IPasswordService PasswordService { get; }
        protected IUserCommandRepository UserCommandRepository { get; }

        public async ValueTask<ServiceResult> RegisterAsync(IRegisterEntity entity)
        {
            var serviceResult = await ValidateEntityAsync(entity);
            if (!serviceResult.IsSuccess)
            {
                return serviceResult;
            }

            var passwordResult = PasswordService.EncryptPassword(entity.Password);
            if (!passwordResult.IsSuccess)
            {
                return new ServiceResult(passwordResult.Errors);
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = entity.Username.ToLower(),
                Password = passwordResult.Result.Password,
                PasswordSalt = passwordResult.Result.Salt,
                IsActive = true,
                PasswordExpiredDate = PasswordExpiredConfigs.EnablePasswordExpired
                    ? DateTime.UtcNow.AddDays(PasswordExpiredConfigs.DaysOfPasswordExpired) : null,
            };

            await UserCommandRepository.AddAsync(user);
            await UnitOfWork.CommitAsync();
            return new ServiceResult(true, user.Id);
        }

        protected async ValueTask<ServiceResult> ValidateEntityAsync(IRegisterEntity entity)
        {
            if (await UserCommandRepository.AnyAsync(w => w.Username == entity.Username))
            {
                return new ServiceResult($"Username : '{entity.Username}' is exist.");
            }

            return new ServiceResult(true);
        }
    }
}