using Common.API.ApiMapping.AiAgents.Models;
using Common.API.ApiMapping.Workflows.Models;

namespace Common.API.ApiMapping.Projects.Models;

public class ProjectResponse
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Services { get; set; } = null!;

    public string Website { get; set; } = null!;

    public string? CompanyName { get; set; } = null!;

    public int IndustryId { get; set; }

    public string? FacebookUrl { get; set; }

    public string? TiktokUrl { get; set; }

    public string? Xurl { get; set; }

    public string? LinkedInUrl { get; set; }

    public string? InstagramUrl { get; set; }

    public string? IdealCustomer { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<ProjectWorkflowResponse> Workflows { get; set; }

    public class ProjectWorkflowResponse : WorkflowResponse
    {
        public virtual ICollection<AiAgentResponse> AiAgents { get; set; } = new List<AiAgentResponse>();
    }
}