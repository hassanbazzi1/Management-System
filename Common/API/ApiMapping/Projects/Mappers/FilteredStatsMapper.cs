using Common.API.ApiMapping.Projects.Models;

namespace Common.API.ApiMapping.Analytics.Mappers
{
    // Added 'partial' to resolve CS0260
    public partial class FilteredStatsMapper
    {
        public FilteredStatsResult Map(
            (int sessions, int messages) counts,
            FilteredStatsRequest request,
            int month)
            => new()
            {
                PageId   = request.PageId,
                Month    = month,
                AgentId  = request.AgentId,
                Sessions = counts.sessions,
                Messages = counts.messages
            };
    }
}
