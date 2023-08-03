using HR.Common.DALs.Repositories.Bases.Queries;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.Commons;

namespace HR.Common.DALs.Repositories.Commons.Languages.Queries
{
    public class LanguageQueryRepository : QueryRepository<Language, IHRDbContext>, ILanguageQueryRepository
    {
        public LanguageQueryRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}