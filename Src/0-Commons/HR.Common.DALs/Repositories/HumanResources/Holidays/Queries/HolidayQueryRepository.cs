using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.Holidays.Queries
{
    public class HolidayQueryRepository : QueryRepository<Holiday, IHRDbContext>, IHolidayQueryRepository
    {
        public HolidayQueryRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}