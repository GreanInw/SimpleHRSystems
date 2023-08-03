using Microsoft.AspNetCore.Mvc;

namespace HR.Common.DTOs.Bases
{
    public class PagingFromQueryBase : PagingBase
    {
        [FromQuery(Name = "limit")]
        public override int Limit { get => base.Limit; set => base.Limit = value; }
        [FromQuery(Name = "page_number")]
        public override int PageNumber { get => base.PageNumber; set => base.PageNumber = value; }
    }
}