namespace Common.API.ApiMapping.Projects.Models
{
    public class FilteredStatsResult
    {
        public int PageId { get; init; }
        public int Month { get; init; }
        public int? AgentId { get; init; }
        public int Sessions { get; init; }
        public int Messages { get; init; }
    }
}
