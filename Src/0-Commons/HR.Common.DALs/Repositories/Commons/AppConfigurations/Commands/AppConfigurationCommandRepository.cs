using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.Commons;

namespace HR.Common.DALs.Repositories.Commons.AppConfigurations.Commands
{
    public class AppConfigurationCommandRepository : CommandRepository<AppConfiguration, IHRDbContext>
        , IAppConfigurationCommandRepository
    {
        public AppConfigurationCommandRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}
