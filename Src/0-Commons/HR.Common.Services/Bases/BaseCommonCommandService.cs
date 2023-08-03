using HR.Common.DALs.UnitOfWorks.Bases;
using Microsoft.AspNetCore.Http;

namespace HR.Common.Services.Bases
{
    public abstract class BaseCommonCommandService<TUnitOfWork> : BaseCommonService
        where TUnitOfWork : IUnitOfWork
    {
        protected BaseCommonCommandService(IHttpContextAccessor httpContextAccessor, TUnitOfWork unitOfWork)
            : base(httpContextAccessor)
        {
            UnitOfWork = unitOfWork;
        }

        protected TUnitOfWork UnitOfWork { get; }
    }
}