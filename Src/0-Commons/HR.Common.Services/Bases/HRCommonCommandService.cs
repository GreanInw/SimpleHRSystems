using HR.Common.DALs.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace HR.Common.Services.Bases
{
    public abstract class HRCommonCommandService : BaseCommonCommandService<IHRUnitOfWork>
    {
        protected HRCommonCommandService(IHttpContextAccessor httpContextAccessor, IHRUnitOfWork unitOfWork)
            : base(httpContextAccessor, unitOfWork)
        { }
    }
}
