using ClientAPIServer.Common.Controllers;
using Common.API.ApiMapping.Projects.Models;
using Common.API.Models;
using Common.Auth;
using Common.DB;
using Common.DB.ClientDB;
using Common.DB.ClientDB.Models;
using Common.DB.ClientDB.Services;
using Common.DB.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiToProjectMapper = Common.API.ApiMapping.Projects.Mappers.ApiToProjectMapper;
using ProjectToApiMapper = Common.API.ApiMapping.Projects.Mappers.ProjectToApiMapper;

namespace ClientAPIServer.Features.Public.Projects.Controllers;

[ApiController]
[Authorize]
[Route(ApiBase + "/" + ApiVersion + "/" + ApiSub)]
public class ProjectsController : ClientAPIController
{
    private const string ApiBase = "api";
    private const string ApiSub = "projects";
    private const string ApiVersion = "v1";

    private readonly ProjectDbService _dbService;

    public ProjectsController(
         ClientDbContext clientDbContext,
        ProjectDbService projectDbService
        )   
         : base(clientDbContext)
    {
        _dbService = projectDbService;
    }

    [HttpGet]
    [Authorize(Roles = AuthConfig.RolesUser)]
    [Authorize(Roles = AuthConfig.RolesAdmin)]
    public async Task<ActionResult<ApiResponse<List<ProjectResponse>>>> GetProjects()
    {
        var response = new ApiResponse<List<ProjectResponse>>();
        try
        {
            // TODO: Make separate action to get limited project info?
            var projects = await _dbService.GetAllAsync(true);
            var mapper = new ProjectToApiMapper();
            var data = projects.Select(x => mapper.MapNew(x)).ToList();
            response.SetStatusSuccess(data);
            return Ok(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            response.SetStatusFailure("Error getting projects");
        }

        return BadRequest(response);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = AuthConfig.RolesUser)]
    [Authorize(Roles = AuthConfig.RolesAdmin)]
    public async Task<ActionResult<ApiResponse<ProjectResponse>>> GetProject([FromRoute] int id)
    {
        var response = new ApiResponse<ProjectResponse>();
        try
        {
            var project = await _dbService.GetByIdAsync(id);
            if (project == null)
                throw new Exception("Project not found");

            var data = new ProjectToApiMapper().MapNew(project);

            response.SetStatusSuccess(data);
            return Ok(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            response.SetStatusFailure("Error getting projects");
        }

        return BadRequest(response);
    }

    [HttpPost]
    [Authorize(Roles = AuthConfig.RolesAdmin)]
    public async Task<ActionResult<ApiResponse<int>>> CreateProject(CreateUpdateProjectRequest request)
    {
        var response = new ApiResponse<int>();
        try
        {
            // TODO: Data Validation
            var project = new ApiToProjectMapper().MapNew(request);

            await _dbService.AddAsync(project);
            response.SetStatusSuccess(project.Id);
            // TODO: Create default workflow and agents
            return Ok(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            response.SetStatusFailure("Error creating project");
        }

        return BadRequest(response);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = AuthConfig.RolesAdmin)]
    public async Task<ActionResult<CommonApiResponse>> UpdateProject([FromRoute] int id, [FromBody] CreateUpdateProjectRequest request)
    {
        // TODO: Can be implemented with ExecuteUpdate
        // TODO: Should source and target names be changed for mapping?
        var response = new CommonApiResponse();
        try
        {
            // TODO: Data Validation
            var project = await _dbService.GetByIdAsync(id);
            if (project == null)
                throw new Exception("Project not found");

            new ApiToProjectMapper().MapExisting(request, project);

            await _dbService.UpdateAsync(project);

            // TODO: Should existing workflows be updated? i.e. new website, etc.
            response.SetStatusSuccess();
            return Ok(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            response.SetStatusFailure("Error updating project");
        }

        return BadRequest(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = AuthConfig.RolesAdmin)]
    public async Task<ActionResult<CommonApiResponse>> DeleteProject([FromRoute] int id)
    {
        var response = new CommonApiResponse();
        try
        {
            await _dbService.DeleteAsync(id);
            response.SetStatusSuccess();
            return Ok(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            response.SetStatusFailure("Error deleting project");
        }

        return BadRequest(response);
    }
}