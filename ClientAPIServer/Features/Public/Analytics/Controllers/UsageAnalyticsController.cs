using Common.API.ApiMapping.Projects.Models;
using Common.DB.ClientDB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientAPIServer.Features.Public.Analytics.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsageAnalyticsController : ControllerBase
    {
        private readonly ProjectDbService _projects;

        public UsageAnalyticsController(ProjectDbService projects) => _projects = projects;

        
        [HttpGet("filtered")]
        public async Task<ActionResult<FilteredStatsResult>> GetFiltered(
            [FromQuery] FilteredStatsRequest request)
        {
            if (request.PageId <= 0) return BadRequest("pageId is required");

            var dto = await _projects.GetFilteredStatsAsync(request);
            return Ok(dto);
        }
    }
}
