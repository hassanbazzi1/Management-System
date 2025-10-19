using ClientAPIServer.Common.Controllers;
using Common.API.ApiMapping.Conversations.Mappers;
using Common.API.ApiMapping.Conversations.Models;
using Common.API.ApiMapping.Projects.Models;
using Common.API.Models;
using Common.Auth;
using Common.DB.ClientDB;
using Common.DB.ClientDB.Models;
using Common.DB.ClientDB.Services;
using Common.DB.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientAPIServer.Features.Public.Conversations.Controllers;

[ApiController]
[Authorize]
[Route(ApiBase + "/" + ApiVersion + "/" + ApiSub)]
public class ConversationsController : ClientAPIController
{
    private const string ApiBase = "api";
    private const string ApiSub = "conversations";
    private const string ApiVersion = "v1";

    private const string RouteGetConversations = "";
    private const string RouteGetChatsForConversation = "{id}";
    
    private readonly ConversationDbService _dbService;

    public ConversationsController(ClientDbContext clientDbContext) : base(clientDbContext)
    {
        _dbService = new ConversationDbService(clientDbContext);
    }

    // TODO: Add filter (search) capability
    // TODO: Can this be get
    [HttpPost]
    [Route(RouteGetConversations)]
    [Authorize(Roles = AuthConfig.RolesUser)]
    [Authorize(Roles = AuthConfig.RolesAdmin)]
    public async Task<ActionResult<ApiResponse<List<ConversationResponse>>>> GetConversations(DataQueryOptions? filter)
    {
        // TODO: Set default filter limit if not set
        var response = new ApiResponse<List<ConversationResponse>>();
        try
        {
            var conversations = await _dbService.GetAllConversationsAsync(filter);
            var mapper = new ConversationToApiMapper();
            var data = conversations.Select(x => mapper.MapNew(x)).ToList();
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
    [Route(RouteGetChatsForConversation)]
    [Authorize(Roles = AuthConfig.RolesUser)]
    [Authorize(Roles = AuthConfig.RolesAdmin)]
    public async Task<ActionResult<ApiResponse<List<ConversationChatsResponse>>>> GetChatsForConversation(DataQueryOptions? filter,[FromRoute]int id)
    {
        // TODO: Set default filter limit if not set
        var response = new ApiResponse<List<ConversationChatsResponse>>();
        try
        {
            var data = await _dbService.GetChatsForConversationApi(filter,id);
            response.SetStatusSuccess(data);
            return Ok(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            response.SetStatusFailure("Error getting conversations");
        }

        return BadRequest(response);
    }
    /*
    [HttpPost]
    [Authorize(Roles = AuthConfig.RolesAdmin)]
    public async Task<ActionResult<ApiResponse<int>>> CreateProject(CreateUpdateProjectRequest request)
    {
        return BadRequest();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = AuthConfig.RolesAdmin)]
    public async Task<ActionResult<CommonApiResponse>> UpdateProject([FromRoute] int id, [FromBody] CreateUpdateProjectRequest request)
    {
        return BadRequest();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = AuthConfig.RolesAdmin)]
    public async Task<ActionResult<CommonApiResponse>> DeleteProject([FromRoute] int id)
    {
        return BadRequest();
    }*/
}