using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.Holidays.Commands
{
    public class HolidayCommandRepository : CommandRepository<Holiday, IHRDbContext>, IHolidayCommandRepository
    {
        public HolidayCommandRepository(IHRDbContext dbContext) : base(dbContext)
        {
        }
    }
}