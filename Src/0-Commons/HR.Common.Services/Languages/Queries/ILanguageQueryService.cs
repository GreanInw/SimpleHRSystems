using HR.Common.EFCores.Queries;
using HR.Common.Models.Commons;
using HR.Common.Services.Bases;

namespace HR.Common.Services.Languages.Queries
{
    public interface ILanguageQueryService : IBaseService, IAnyAsyncQuery<Language>
    {
        ValueTask<Language> GetByAsync(int id);
        ValueTask<Language> GetByCodeAsync(string code);
    }
}