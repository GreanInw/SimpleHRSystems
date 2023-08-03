namespace HR.Common.DTOs.Bases
{
    public abstract class PagingBase : IPaging
    {
        private int _limit;
        private int _page;

        public virtual int Limit { get => _limit < 0 ? 10 : _limit; set => _limit = value; }
        public virtual int PageNumber { get => _page < 0 ? 1 : _page; set => _page = value; }
    }
}