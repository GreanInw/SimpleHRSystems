using HR.Common.DALs.Repositories.Bases.Commands;
using HR.Common.DbContexts.Contexts;
using HR.Common.Models.Commons;

namespace HR.Common.DALs.Repositories.Commons.Languages.Commands
{
    public class LanguageCommandRepository : CommandRepository<Language, IHRDbContext>, ILanguageCommandRepository
    {
        public LanguageCommandRepository(IHRDbContext dbContext) : base(dbContext)
        { }
    }
}