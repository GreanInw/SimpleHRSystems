using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.Positions.Queries
{
    public class PositionQueryRepository : QueryRepository<Position, IHRDbContext>, IPositionQueryRepository
    {
        public PositionQueryRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}