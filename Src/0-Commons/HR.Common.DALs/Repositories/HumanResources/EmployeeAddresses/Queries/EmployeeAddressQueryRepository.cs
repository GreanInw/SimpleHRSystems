using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.EmployeeAddresses.Queries
{
    public class EmployeeAddressQueryRepository : QueryRepository<EmployeeAddress, IHRDbContext>, IEmployeeAddressQueryRepository
    {
        public EmployeeAddressQueryRepository(IHRDbContext dbContext) : base(dbContext)
        {
        }
    }
}