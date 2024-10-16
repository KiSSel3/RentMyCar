namespace CarManagementService.Application.Helpers;

public class PagedList<T> : List<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get;  set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedList()
    {
        TotalCount = 0;
        PageSize = 0;
        CurrentPage = 0;
        TotalPages = 0;
    }
    
    public PagedList(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
    {
        AddRange(items);
        
        TotalCount = totalCount;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}