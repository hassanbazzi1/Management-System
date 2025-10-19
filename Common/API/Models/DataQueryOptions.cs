namespace Common.API.Models;

public class DataQueryOptions
{
    public PageOptions? PageOptions {get;set;}
    public SortOptions? SortOptions {get;set;}
    
    public FilterOptions? FilterOptions {get;set;}
    
    public bool HasValidSortOptions()
    {
        return SortOptions != null && SortOptions.OrderBy.Count > 0;
    }
    
    public bool HasValidFilterOptions()
    {
        return FilterOptions != null && FilterOptions.SearchText!=null && FilterOptions.SearchText.Length>0;
    }
    
    public bool HasValidPageOptions()
    {
        return IsKeysetPagination() || IsOffsetPagination();
    }

    public bool IsOffsetPagination()
    {
        return PageOptions != null && PageOptions.Position != null && PageOptions.Position >= 0;
    }
    
    public bool IsKeysetPagination()
    {
        return PageOptions != null && PageOptions.LastId != null && PageOptions.LastId > 0;
    }
}