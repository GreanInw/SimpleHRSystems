using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.HumanResources;

namespace HR.Common.DALs.Repositories.HumanResources.EmployeeAddresses.Commands
{
    public class EmployeeAddressCommandRepository : CommandRepository<EmployeeAddress, IHRDbContext>, IEmployeeAddressCommandRepository
    {
        public EmployeeAddressCommandRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}