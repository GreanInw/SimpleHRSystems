namespace HR.Common.DTOs.Bases
{
    public interface IPaging
    {
        int Limit { get; set; }
        int PageNumber { get; set; }
    }
}