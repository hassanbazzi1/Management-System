using Common.Auth;
using Common.DB;
using Common.DB.ClientDB;
using Common.DB.ClientDB.Factories;
using Common.DB.ClientDB.Models;
using Common.DB.ClientDB.Services;
using Common.DB.Common;
using Common.DB.Global;
using Common.DB.MasterDB;
using Common.DB.MasterDB.Models;
using Common.JSON;
using Common.Misc;
using Common.N8N.Defaults.Services;
using Common.N8N.Misc;
using MainServer.Common.Models;
using MainServer.Common.Services;
using MainServer.Features.Signup.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using AiAgent = Common.DB.ClientDB.Models.AiAgent;

namespace MainServer.Features.Signup.Services;

public abstract class SignupService : IdentityUserService
{
    private readonly ClientTemplateDbService _clientTemplateDbService;
    private readonly ClientDbContextFactory _dbContextFactory;
    private readonly N8NDefaultValuesService _n8NDefaultValuesService;
    private readonly N8NWorkflowDefinitionUpdateService _n8NWorkflowDefinitionUpdateService;
    private readonly IOptionsMonitor<SignupConfig> _signupConfig;
    protected readonly GlobalDbContext GlobalContext;

    protected SignupService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, GlobalDbContext globalContext, MasterDbContext masterContext, ClientTemplateDbService clientTemplateDbService, EmailSender emailSender, ClientDbContextFactory dbContextFactory, N8NDefaultValuesService n8NDefaultValuesService, N8NWorkflowDefinitionUpdateService n8NWorkflowDefinitionUpdateService, IOptionsMonitor<SignupConfig> signupConfig) : base(userManager, signInManager, roleManager)
    {
        GlobalContext = globalContext;
        MasterDbContext = masterContext;
        _clientTemplateDbService = clientTemplateDbService;
        EmailSender = emailSender;
        _dbContextFactory = dbContextFactory;
        _n8NDefaultValuesService = n8NDefaultValuesService;
        _n8NWorkflowDefinitionUpdateService = n8NWorkflowDefinitionUpdateService;
        _signupConfig = signupConfig;
    }

    public MasterDbContext MasterDbContext { get; }

    public EmailSender EmailSender { get; }

    private string GetNewClientDatabaseName(Client client)
    {
        return "client_" + client.Name.Replace(" ", "_").ToLower();
    }

    protected async Task<DataActionResult<ClientServerConfig>> GetTargetClientServerCredentials(Client client)
    {
        // TODO: Not implemented
        // TODO: Add service to determine which server to host on
        var result = new DataActionResult<ClientServerConfig>();
        try
        {
            var serverConfig = new ClientServerConfig();
            var credentials = new DbCredentials
            {
                DbName = GetNewClientDatabaseName(client),
                Host = "localhost",
                Port = 5432,
                Username = "postgres",
                Password = "postgres"
            };

            serverConfig.Credentials = credentials;

            serverConfig.Server = await MasterDbContext.Servers.FirstOrDefaultAsync(s => s.Id != 0);

            result.Data = serverConfig;
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

    protected abstract Task<DataActionResult<ClientServerConfig>> CreateNewClientDatabase(Client client);

    protected abstract Task<DataActionResult<string>> CreateNewClientTables(ClientDbContext context);

    public async Task<DataActionResult<Client>> InitializeClientDatabase(Client client, int subscriptionType)
    {
        var result = new DataActionResult<Client>();
        try
        {
            // TODO: Handle failures -- i.e. rollback (transactions?)
            // Initialize client database
            var createResult = await CreateNewClientDatabase(client);
            if (createResult.IsSuccess())
            {
                // Assign server to client
                client.ServerId = createResult.Data.Server.Id;

                // Create Client Context from credentials
                var clientContext = _dbContextFactory.CreateDbContext(createResult.Data.Credentials);

                await using var transaction = await clientContext.Database.BeginTransactionAsync();
                {
                    // Create client tables
                    var createTableResult = await CreateNewClientTables(clientContext);
                    if (createTableResult.IsSuccess())
                    {
                        // Clone template into client database
                        var cloneResult = await _clientTemplateDbService.CloneDatabaseFromTemplate(clientContext);
                        if (cloneResult.IsSuccess())
                        {
                            // Initialize defaults
                            var initResult = await InitializeClientDatabaseDefaults(client, subscriptionType, clientContext);

                            await transaction.CommitAsync();

                            if (initResult.IsSuccess())
                            {
                                // Generate n8n workflow
                                // Get n8n default values if not done already
                                if (!_n8NDefaultValuesService.HasDefaultValues())
                                    await _n8NDefaultValuesService.GetAllDefaultValues();


                                //_n8nServiceCollection.N8NAPIService.CreateWorkflow(initResult.Data.Name,_n8NDefaultValuesService.DefaultValues.)
                            }
                            else
                            {
                                result.AddChildResult(initResult);
                                throw new Exception("Adding client database defaults failed");
                            }
                        }
                        else
                        {
                            result.AddChildResult(cloneResult);
                            throw new Exception("Cloning client database failed");
                        }
                    }
                    else
                    {
                        result.AddChildResult(createTableResult);
                        throw new Exception("Creation of client tables failed");
                    }
                }
            }
            else
            {
                result.AddChildResult(createResult);
                throw new Exception("Create client database failed");
            }
            
            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Exception = e;
            result.Status = ActionResult.ActionStatus.Failure;
        }

        return result;
    }

    public async Task<DataActionResult<Workflow>> InitializeClientDatabaseDefaults(Client client, int subscriptionType, ClientDbContext clientContext)
    {
        var result = new DataActionResult<Workflow>();
        try
        {
            // Create first/default client subscription
            var subscription = new Subscription
            {
                TypeId = subscriptionType,
                Name = "First Subscription",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow + TimeSpan.FromHours(_signupConfig.CurrentValue.TrialPeriodHours),
                IsActive = false
            };

            clientContext.Subscriptions.Add(subscription);
            await clientContext.SaveChangesAsync();

            // Default Workflow
            // TODO: Can make this fetch a default workflow from n8n API to better handle changes and allow
            var defaultWorkflow = new Workflow
            {
                Name = "Default Workflow",
                Description = "My first workflow",
                Url = null,
                CreateDate = DateTime.UtcNow,
                Json = _n8NDefaultValuesService.DefaultValues.DefaultWorkflowJson
            };

            clientContext.Workflows.Add(defaultWorkflow);
            await clientContext.SaveChangesAsync();

            // Default Instructions
            var defaultInstructions = new Instructions
            {
                Name = "Default Instructions",
                Description = "Default Instructions",
                CreateDate = DateTime.UtcNow
            };

            clientContext.Instructions.Add(defaultInstructions);
            await clientContext.SaveChangesAsync();

            // Default Instructions Sections
            var systemMessageJson = _n8NDefaultValuesService.DefaultValues.DefaultSystemMessageJson;
            systemMessageJson = systemMessageJson.Replace(_n8NDefaultValuesService.DefaultValues.CompanyNamePlaceholder, client.Name);
            systemMessageJson = systemMessageJson.Replace(_n8NDefaultValuesService.DefaultValues.CompanyServicesPlaceholder, client.Services);
            systemMessageJson = systemMessageJson.Replace(_n8NDefaultValuesService.DefaultValues.CompanyWebsitePlaceholder, client.Website);
            var systemMessage = JsonUtil.LoadJsonObjectFromFile<SystemMessageWrapper>(systemMessageJson);

            var i = 1;
            foreach (var section in systemMessage.SystemMessage)
            {
                clientContext.InstructionsSections.Add(new InstructionsSection
                {
                    Iid = defaultInstructions.Id,
                    Priority = 1,
                    Order = i,
                    Name = section.Name,
                    Description = section.Name,
                    Text = section.Value,
                    CreateDate = DateTime.UtcNow
                });

                i++;
            }

            await clientContext.SaveChangesAsync();

            // Default Rules and RuleSets
            var rulesets = JsonUtil.LoadJsonObjectFromFile<RulesContainer>(_n8NDefaultValuesService.DefaultValues.DefaultRulesJson);
            var ruleMap = new Dictionary<RuleSet, List<Rule>>();
            foreach (var ruleset in rulesets.Rulesets)
            {
                var rules = new List<Rule>();
                var rs = new RuleSet
                {
                    Name = ruleset.Name,
                    Description = ruleset.Name,
                    CreateDate = DateTime.UtcNow
                };

                clientContext.RuleSets.Add(rs);

                i = 1;
                foreach (var rule in ruleset.Rules)
                {
                    var r = new Rule
                    {
                        Text = rule,
                        Description = "Rule" + i,
                        CreateDate = DateTime.UtcNow,
                        Name = "Rule " + i
                    };
                    rules.Add(r);
                    clientContext.Rules.Add(r);
                    i++;
                }

                ruleMap.Add(rs, rules);
            }

            await clientContext.SaveChangesAsync();

            // Default RuleSetRules and InstructionRules
            foreach (var keyValue in ruleMap)
            foreach (var rule in keyValue.Value)
            {
                clientContext.RuleSetRules.Add(new RuleSetRule
                {
                    Rid = rule.Id,
                    Rsid = keyValue.Key.Id
                });

                clientContext.InstructionsRules.Add(new InstructionsRule
                {
                    Iid = defaultInstructions.Id,
                    Rid = rule.Id,
                    Priority = 1,
                    CreateDate = DateTime.UtcNow
                });
            }

            await clientContext.SaveChangesAsync();

            // Default AI Agent
            var defaultAgent = new AiAgent
            {
                Code = "default",
                Wid = defaultWorkflow.Id,
                Name = "Default AI Agent",
                Description = "My first AI agent",
                CreateDate = DateTime.UtcNow,
                InstructionsId = defaultInstructions.Id
            };

            clientContext.AiAgents.Add(defaultAgent);
            await clientContext.SaveChangesAsync();

            // Default Chat Report Formats
            var defaultFormat = new ChatReportFormat
            {
                Code = "sheets",
                Name = "Google Sheets",
                Description = "Represents a chat report in Google Sheets"
            };

            clientContext.ChatReportFormats.Add(defaultFormat);
            await clientContext.SaveChangesAsync();

            // Default Chat Report Templates
            var defaultTemplate = new ChatReportTemplate
            {
                Name = "Default Template",
                Description = "Default Chat Report template",
                FormatId = defaultFormat.Id,
                CreateDate = DateTime.UtcNow
            };

            clientContext.ChatReportTemplates.Add(defaultTemplate);

            // Default Chat Report Column Types
            var columnTypes = JsonUtil.LoadJsonObjectFromString<ColumnTypeContainer>(_n8NDefaultValuesService.DefaultValues.DefaultTemplateColumnTypeJson);
            foreach (var columnType in columnTypes.ColumnTypes)
                clientContext.ChatReportTemplateColumnTypes.Add(new ChatReportTemplateColumnType
                {
                    Name = columnType.Name,
                    Description = columnType.Description
                });
            await clientContext.SaveChangesAsync();

            var columnMap = clientContext.ChatReportTemplateColumnTypes.ToDictionary(x => x.Name, x => x);

            // Default Chat Report Columns
            var columns = JsonUtil.LoadJsonObjectFromString<ColumnContainer>(_n8NDefaultValuesService.DefaultValues.DefaultTemplateColumnJson);
            foreach (var column in columns.Columns)
                clientContext.ChatReportTemplateColumns.Add(new ChatReportTemplateColumn
                {
                    TemplateId = defaultTemplate.Id,
                    TypeId = columnMap[column.Type].Id,
                    Name = column.Name,
                    Description = column.Description,
                    CreateDate = DateTime.UtcNow
                });
            await clientContext.SaveChangesAsync();
            result.Data = defaultWorkflow;
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

    public async Task<DataActionResult<Client>> CreateClientEntity(int industryID, string businessEmail, string phone, string services, string websiteUrl, string companyName, string? facebookUrl, string? tiktokUrl, string? xUrl, string? linkedInUrl, string? instagramUrl, string idealCustomer, string identityUserId)
    {
        // TODO: Create subscription in client server
        var result = new DataActionResult<Client>();
        try
        {
            // Create client
            var client = new Client
            {
                Name = companyName,
                CreateDate = DateTime.UtcNow,
                Email = businessEmail,
                Phone = phone,
                IndustryId = industryID,
                Services = services,
                Website = websiteUrl,
                CompanyName = companyName,
                FacebookUrl = facebookUrl,
                TiktokUrl = tiktokUrl,
                Xurl = xUrl,
                LinkedInUrl = linkedInUrl,
                InstagramUrl = instagramUrl,
                IdealCustomer = idealCustomer,
                IsActive = false
            };

            MasterDbContext.Clients.Add(client);

            // Create default user
            var user = new ClientUser
            {
                Client = client,
                Username = "admin",
                Email = businessEmail,
                Phone = phone,
                CreateDate = DateTime.UtcNow,
                // TODO: Should code not be hard coded
                TypeId = MasterDbContext.ClientUserTypes.First(s => s.Code == "admin").Id,
                IsActive = false,
                IdentityUserId = identityUserId
            };

            MasterDbContext.ClientUsers.Add(user);

            await MasterDbContext.SaveChangesAsync();
            result.Data = client;
            result.Status = ActionResult.ActionStatus.Success;
        }
        catch (Exception e)
        {
            result.Exception = e;
            result.Status = ActionResult.ActionStatus.Failure;
        }

        return result;
    }
}