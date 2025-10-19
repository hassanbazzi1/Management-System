using Common.API.ApiMapping.Analytics.Mappers;

using Common.API.ApiMapping.Projects.Models;
using Common.DB.ClientDB;
using Common.DB.ClientDB.Models;
using Common.DB.ClientDB.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ClientAPIServerTest.Tests.Analytics.Storage
{
    // ---- ❶  Supply the “other half” of Mapperly’s partial class -------------
    // ❶  A tiny test-only mapper — NO 'partial', so it can’t conflict
    internal class TestFilteredStatsMapper
    {
        public FilteredStatsResult Map(
            (int sessions, int messages) counts,
            FilteredStatsRequest req,
            int month) =>
            new()
            {
                PageId   = req.PageId,
                Month    = month,
                AgentId  = req.AgentId,
                Sessions = counts.sessions,
                Messages = counts.messages
            };
    }

    // -------------------------------------------------------------------------

    public class ProjectTest
    {
        private static ClientDbContext BuildContext(string dbName)
        {
            var opt = new DbContextOptionsBuilder<ClientDbContext>()
                      .UseInMemoryDatabase(dbName)
                      .Options;
            return new ClientDbContext(opt);
        }
        [Fact]
        public async Task GetFilteredStatsAsync_ReturnsCorrectCounts()
        {
            // ---------- Arrange ---------------------------------------------
            var ctx = BuildContext(nameof(GetFilteredStatsAsync_ReturnsCorrectCounts));

            // 1) Seed a “page” with all required fields
            var proj = new Project
            {
                Id         = 1,
                Name       = "Page 1",
                Email      = "test@example.com",
                Phone      = "1234567890",
                Website    = "https://example.com",
                IndustryId = 0,              // if you have no Industry FK enforcement, this is fine
                IsDeleted  = false,
                // CreateDate will default to DateTime.MinValue which is allowed
            };
            ctx.Projects.Add(proj);

            // 2) Workflow under that page
            var wf = new Workflow
            {
                Id        = 10,
                ProjectId = proj.Id,
                Name      = "WF10",
                IsDeleted = false,
            };
            ctx.Workflows.Add(wf);

            // 3) Agent attached to that workflow
            var agent = new AiAgent
            {
                Id         = 100,
                Workflow   = wf,
                Name       = "Bot",
                Code       = "BOT100",
                CreateDate = DateTime.UtcNow,
                InstructionsId = 0,
                IsDeleted  = false
            };
            ctx.AiAgents.Add(agent);

            // 4) Conversation for that agent
            var conv = new Conversation
            {
                Id         = 1,
                AiAgent    = agent,
                CreateDate = DateTime.UtcNow,
                IsDeleted  = false
            };
            ctx.Conversations.Add(conv);

            // 5) A session in the current month
            var today = DateTime.UtcNow;
            var sess = new Session
            {
                Id             = 50,
                Conversation   = conv,
                StartDate      = today.AddDays(-1),
                EndDate        = today,
                IsDeleted      = false
            };
            ctx.Sessions.Add(sess);

            // 6) One chat and two messages
            var chat = new Chat
            {
                Id            = 7,
                Conversation  = conv,
                StartDate     = today.AddHours(-1),
                EndDate       = today,
                IsDeleted     = false
            };
            ctx.Chats.Add(chat);
            ctx.ChatMessages.AddRange(
                new ChatMessage { Id = 1, Chat = chat, Text = "hi", Date = today, IsUser = true, IsDeleted = false },
                new ChatMessage { Id = 2, Chat = chat, Text = "hello", Date = today, IsUser = false, IsDeleted = false }
            );

            await ctx.SaveChangesAsync();

            // service + a simple mapper (no partial duplication in test)
            var mapper = new FilteredStatsMapper();
            var service = new ProjectDbService(ctx, mapper);

            var req = new FilteredStatsRequest
            {
                PageId  = 1,
                Month   = today.Month,
                AgentId = 100
            };

            // ---------- Act ---------------------------------------------------
            var dto = await service.GetFilteredStatsAsync(req);

            // ---------- Assert ------------------------------------------------
            Assert.Equal(1, dto.Sessions);
            Assert.Equal(2, dto.Messages);
            Assert.Equal(req.PageId, dto.PageId);
            Assert.Equal(req.Month, dto.Month);
            Assert.Equal(req.AgentId, dto.AgentId);
        }
    }
}
