using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.Positions.Commands
{
    public class PositionCommandRepository : CommandRepository<Position, IHRDbContext>, IPositionCommandRepository
    {
        public PositionCommandRepository(IHRDbContext dbContext) : base(dbContext)
        {
        }
    }
}
