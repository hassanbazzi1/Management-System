namespace Common.API.Models;

public class FilterOptions
{
    public string? SearchText { get; set; }
    
    public List<string>? IncludedFields { get; set; }  
}