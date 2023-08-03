using HR.Common.DALs.Repositories.Commons.AppConfigurations.Queries;
using HR.Common.Models.Commons;
using HR.Common.Services.AppConfigurations.Helpers;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HR.Common.Services.AppConfigurations.Queries
{
    public class AppConfigurationQueryService : BaseCommonService
        , IAppConfigurationQueryService
    {
        public AppConfigurationQueryService(IHttpContextAccessor httpContextAccessor
            , IAppConfigurationQueryRepository appConfigurationQueryRepository) : base(httpContextAccessor)
        {
            AppConfigurationQueryRepository = appConfigurationQueryRepository;
        }

        protected IAppConfigurationQueryRepository AppConfigurationQueryRepository { get; }

        public async ValueTask<AppConfiguration> GetByNameAsync(string name)
            => await AppConfigurationQueryRepository.All.AsNoTracking()
                .FirstOrDefaultAsync(w => w.IsActive && w.Name == name);

        public async ValueTask<IEnumerable<AppConfiguration>> GetByNamesAsync(params string[] names)
            => await AppConfigurationQueryRepository.All.AsNoTracking()
                .Where(w => w.IsActive && names.Contains(w.Name)).ToListAsync();

        public async ValueTask<TConfigs> BindConfigurationAsync<TConfigs>()
            where TConfigs : class, new()
        {
            var names = AppConfigurationBinderHelper.GetConfigurationNames(typeof(TConfigs));
            var configs = await GetByNamesAsync(names.ToArray());
            return AppConfigurationBinderHelper.Bind<TConfigs>(configs);
        }
    }
}