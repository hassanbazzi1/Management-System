using ClientAPIServer.Common.Controllers;
using Common.API.ApiMapping.Sessions.Models;
using Common.DB.ClientDB;
using Common.DB.ClientDB.Models;
using Common.DB.ClientDB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientAPIServer.Features.Public.Dashboard.Controllers
{
    [Route("api")]
    [ApiController]
    public class DashboardController : ClientAPIController
    {

        private readonly ChatDbService _chatDbService;
        private readonly SessionDbService _sessionDbService;
        private readonly ChatEscalationDbService _chatEscalationDbService;
        private readonly AIAgentDbService _aiAgentDbService;
        private readonly ConversationDbService _conversationDbService;
        public DashboardController(ClientDbContext clientDbContext, ChatDbService chatDbService , SessionDbService sessionDbService , ChatEscalationDbService chatEscalationDbService,AIAgentDbService aIAgentDbService, ConversationDbService conversationDbService) : base(clientDbContext)
        {
            _chatDbService = chatDbService;
            _sessionDbService = sessionDbService;
            _chatEscalationDbService = chatEscalationDbService;
            _aiAgentDbService = aIAgentDbService;
            _conversationDbService = conversationDbService;
        }
       
        [HttpGet("monthly-summary")]
        public async Task<IActionResult> MonthlySummary()
        {
     
            MonthlySessionsResponse summary = await _sessionDbService.GetYearlyComparisonAsync();

            return Ok(summary);
        }
        [HttpGet("agent/active")]
        public async Task<IActionResult> GetActiveAgents()
        {
            var result = await _conversationDbService.GetActiveAgent();

            return Ok(result);
        }
        [HttpGet("agent/inactive")]
        public async Task<IActionResult> GetInActiveAgents()
        {
            var result = await _conversationDbService.GetInactiveAgent();

            return Ok(result);
        }
        [HttpGet("escalation/all")]
        public async Task<IActionResult> GetAllEscalations()
        {
            var result = await _chatEscalationDbService.GetEscalationsCount();
            return Ok(result);
        }
        [HttpGet("chat/all")]
        public async Task<IActionResult> GetAllChats()
        {
            var result = await _chatDbService.GetChatsCount();
            return Ok(result);
        }

        [HttpGet("session/all")]
        public async Task<IActionResult> GetAllSessions()
        {
            var result = await _sessionDbService.GetAllSessions();
            return Ok(result);
        }

        [HttpGet("session/active")]
        public async Task<IActionResult> GetActiveSessions()
        {
            var result = await _sessionDbService.GetActiveSessions();
            return Ok(result);
        }
        [HttpGet("session/closed")]
        public async Task<IActionResult> GetClosedSessions()
        {
            var result = await _sessionDbService.GetClosedSessions();
            return Ok(result);
        }
    }
}
