using HR.Common.DALs.Repositories.Commons.Languages.Queries;
using HR.Common.Models.Commons;
using HR.Common.Services.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HR.Common.Services.Languages.Queries
{
    public class LanguageQueryService : BaseCommonService, ILanguageQueryService
    {
        public LanguageQueryService(IHttpContextAccessor httpContextAccessor
            , ILanguageQueryRepository languageQueryRepository) : base(httpContextAccessor)
        {
            LanguageQueryRepository = languageQueryRepository;
        }

        protected ILanguageQueryRepository LanguageQueryRepository { get; }

        public async ValueTask<bool> AnyAsync(Expression<Func<Language, bool>> expression)
            => await LanguageQueryRepository.AnyAsync(expression);

        public async ValueTask<Language> GetByAsync(int id)
            => (await LanguageQueryRepository.GetByIdAsync(id)) 
                ?? await LanguageQueryRepository.All.FirstOrDefaultAsync(w => w.IsDefault);

        public async ValueTask<Language> GetByCodeAsync(string code)
            => await LanguageQueryRepository.All.AsNoTracking()
                .FirstOrDefaultAsync(w => w.Code == code);
    }
}
