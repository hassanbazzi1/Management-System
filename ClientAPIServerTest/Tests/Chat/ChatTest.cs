using Common.DB.ClientDB;
using Common.DB.ClientDB.Models;
using Common.DB.ClientDB.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClientAPIServerTest.Tests.ChatTest
{
    public class ChatTest
    {
        private ClientDbContext CreateContext(string dbName)
        {
            var opts = new DbContextOptionsBuilder<ClientDbContext>()
                       .UseInMemoryDatabase(dbName)
                       .Options;
            return new ClientDbContext(opts);
        }

        private static Chat MakeChat(int agentId, bool active, int convId)
        {
            var now = DateTime.UtcNow;
            return new Chat
            {
                Id            = convId * 10 + agentId,       // arbitrary key
                Conversation  = new Conversation { Id = convId, AiAgentId = agentId },
                EndDate       = active ? now.AddMinutes(+30) : now.AddMinutes(-30),
                IsDeleted     = false
            };
        }

        [Fact]
        public async Task GetActiveAgent_ReturnsDistinctAgentsWithLiveChats()
        {
            var ctx = CreateContext(nameof(GetActiveAgent_ReturnsDistinctAgentsWithLiveChats));

            // Agent 1 -> 1 active chat
            ctx.Chats.Add(MakeChat(agentId: 1, active: true, convId: 1));
            // Agent 2 -> only closed chat
            ctx.Chats.Add(MakeChat(agentId: 2, active: false, convId: 2));
            await ctx.SaveChangesAsync();

            var service = new ConversationDbService(ctx);

            var active = await service.GetActiveAgent();

            Assert.Equal(1, active);  // only agent 1 qualifies
        }

        [Fact]
        public async Task GetInactiveAgent_ReturnsAgentsWithNoLiveChats()
        {
            var ctx = CreateContext(nameof(GetInactiveAgent_ReturnsAgentsWithNoLiveChats));

            // Build two conversations for agents 1 & 2
            ctx.Chats.AddRange(
                MakeChat(agentId: 1, active: true, convId: 1),  // agent 1 active
                MakeChat(agentId: 2, active: false, convId: 2)); // agent 2 inactive
            await ctx.SaveChangesAsync();

            var service = new ConversationDbService(ctx);
            var inactive = await service.GetInactiveAgent();

            Assert.Equal(1, inactive);           // only agent 2
        }
    }
}
