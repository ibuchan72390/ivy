namespace Ivy.Data.Core.Interfaces.Pagination
{
    public interface IPaginationRequest
    {
        int PageNumber { get; set; }

        int PageCount { get; set; }

        string Search { get; set; }

        // Alternative Additions - for grid
        // SortAttribute
        // IsAsc
    }
}
