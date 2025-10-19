using Common.Misc;
using Microsoft.EntityFrameworkCore;

namespace Common.DB.MasterDB.Services;

public abstract class MasterDbService
{
    private readonly MasterDbContext _masterContext;

    public MasterDbService(MasterDbContext masterContext)
    {
        _masterContext = masterContext;
    }

    public async Task<DataActionResult<string>> ClearMasterDatabase()
    {
        var result = new DataActionResult<string>();
        try
        {
            await _masterContext.AiAgents.ExecuteDeleteAsync();
            await _masterContext.AiAgentInstructions.ExecuteDeleteAsync();
            await _masterContext.ClientUsers.ExecuteDeleteAsync();
            await _masterContext.ClientUserTypes.ExecuteDeleteAsync();
            await _masterContext.ClientSettings.ExecuteDeleteAsync();
            await _masterContext.Clients.ExecuteDeleteAsync();
            await _masterContext.Industries.ExecuteDeleteAsync();
            await _masterContext.MessageTemplates.ExecuteDeleteAsync();
            await _masterContext.Settings.ExecuteDeleteAsync();

            // . NET Identity Models
            await _masterContext.Roles.ExecuteDeleteAsync();
            await _masterContext.Users.ExecuteDeleteAsync();
            await _masterContext.UserRoles.ExecuteDeleteAsync();
            await _masterContext.UserLogins.ExecuteDeleteAsync();
            await _masterContext.UserClaims.ExecuteDeleteAsync();
            await _masterContext.UserTokens.ExecuteDeleteAsync();
            await _masterContext.RoleClaims.ExecuteDeleteAsync();
            await _masterContext.Servers.ExecuteDeleteAsync();

            /*
            await _masterContext.RolePermissions.ExecuteDeleteAsync();
            await _masterContext.Permissions.ExecuteDeleteAsync();
            */


            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Message = e.Message;
            result.Status = ActionResult.ActionStatus.Failure;
            result.Exception = e;
        }

        return result;
    }

    public async Task<DataActionResult<string>> InitializeMasterDatabase()
    {
        var result = new DataActionResult<string>();
        try
        {
            // Subscription Types
        }
        catch (Exception e)
        {
            result.Message = e.Message;
            result.Status = ActionResult.ActionStatus.Failure;
            result.Exception = e;
        }

        return result;
    }
}