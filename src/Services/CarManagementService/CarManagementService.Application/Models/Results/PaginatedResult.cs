namespace CarManagementService.Application.Models.Results;

public class PaginatedResult<T>
{
    public IEnumerable<T> Collection { get; set; }
    
    public int CurrentPage { get; set; }
    
    public int PageSize { get; set; }
    
    public int TotalPageCount { get; set; }
    
    public int TotalItemCount { get; set; }
}