using HR.Common.Models.Commons;
using HR.Common.Services.Bases;

namespace HR.Common.Services.AppConfigurations.Queries
{
    public interface IAppConfigurationQueryService : IBaseService
    {
        ValueTask<AppConfiguration> GetByNameAsync(string name);
        ValueTask<IEnumerable<AppConfiguration>> GetByNamesAsync(params string[] names);
        ValueTask<TConfigs> BindConfigurationAsync<TConfigs>() where TConfigs : class, new();
    }
}
