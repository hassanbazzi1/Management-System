using System.Text.RegularExpressions;
using Common.DB;
using Common.DB.ClientDB;
using Common.DB.ClientDB.Factories;
using Common.DB.ClientDB.Services;
using Common.DB.Common;
using Common.DB.Global;
using Common.DB.MasterDB;
using Common.DB.MasterDB.Models;
using Common.File;
using Common.Misc;
using Common.N8N.Defaults.Services;
using Common.N8N.Misc;
using MainServer.Common.Services;
using MainServer.Features.Signup.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MainServer.Features.Signup.Services;

public class PostGreSqlSignupService : SignupService
{
    // TODO: Make relative or pass sql to file
    private const string DefaultSqlCreatePath = @"C:\Users\samij\AI Agent\AIChatbot\Common\DB\Global\Client_Template_Creation_Script.sql";

    public PostGreSqlSignupService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, GlobalDbContext globalContext, MasterDbContext masterContext, ClientTemplateDbService clientTemplateDbService, EmailSender emailSender, ClientDbContextFactory dbContextFactory, N8NDefaultValuesService n8NDefaultValuesService, N8NWorkflowDefinitionUpdateService n8NWorkflowDefinitionUpdateService, IOptionsMonitor<SignupConfig> signupConfig) : base(userManager, signInManager, roleManager, globalContext, masterContext, clientTemplateDbService, emailSender, dbContextFactory, n8NDefaultValuesService, n8NWorkflowDefinitionUpdateService, signupConfig)
    {
    }

    protected override async Task<DataActionResult<ClientServerConfig>> CreateNewClientDatabase(Client client)
    {
        // TODO: Add service to determine which server to host on
        var result = new DataActionResult<ClientServerConfig>();
        try
        {
            // TODO: This can be moved to separate service and then a created context is passed here
            var serverConfigResult = await GetTargetClientServerCredentials(client);
            if (serverConfigResult.IsSuccess())
            {
                if (Regex.IsMatch(serverConfigResult.Data.Credentials.DbName, "^[A-Za-z0-9_]+$"))
                {
                    // Create Database
                    var query = "CREATE DATABASE " + serverConfigResult.Data.Credentials.DbName + " OWNER = postgres ENCODING = 'UTF8' CONNECTION LIMIT = -1;";
                    var queryResult = await GlobalContext.Database.ExecuteSqlRawAsync(query, CancellationToken.None);
                    result.Status = ActionResult.ActionStatus.Success;
                    result.Data = serverConfigResult.Data;
                }
                else
                {
                    throw new Exception("Error getting remote credentials");
                }
            }
            else
            {
                throw new Exception("Error getting remote credentials");
            }
        }
        catch (Exception e)
        {
            result.Message = e.Message;
            result.Status = ActionResult.ActionStatus.Failure;
            result.Exception = e;
        }

        return result;
    }

    protected override async Task<DataActionResult<string>> CreateNewClientTables(ClientDbContext context)
    {
        // TODO: Add service to determine which server to host on
        var result = new DataActionResult<string>();
        try
        {
            var sqlCreateScript = FileUtil.ReadAllText(DefaultSqlCreatePath);
            // TODO: Does this need escaping?
            await context.Database.ExecuteSqlRawAsync(sqlCreateScript);
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