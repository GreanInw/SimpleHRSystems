using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.Commons;

namespace HR.Common.DALs.Repositories.Commons.AppConfigurations.Queries
{
    public class AppConfigurationQueryRepository : QueryRepository<AppConfiguration, IHRDbContext>, IAppConfigurationQueryRepository
    {
        public AppConfigurationQueryRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}