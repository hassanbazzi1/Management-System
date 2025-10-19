namespace Common.API.ApiMapping.Projects.Models
{
    public class FilteredStatsRequest
    {
        public int PageId { get; set; }
        public int? Month { get; set; }
        public int? AgentId { get; set; }
    }
}
