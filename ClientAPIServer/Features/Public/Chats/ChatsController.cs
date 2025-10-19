using ClientAPIServer.Common.Controllers;
using Common.DB.ClientDB;
using Common.DB.ClientDB.Models;
using Common.DB.ClientDB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientAPIServer.Features.Public.Chats
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ClientAPIController
    {
        private readonly ChatDbService _chatDbService;
        public ChatsController(ClientDbContext clientDbContext) : base(clientDbContext)
        {
            _chatDbService = new ChatDbService(clientDbContext);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddChat([FromBody] Chat chat)
        {
            if (chat == null)
            {
                return BadRequest("Chat cannot be null");
            }

            await _chatDbService.AddChatAsync(chat);
            return Ok("Chat added successfully");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllChats()
        {
            var result = await _chatDbService.GetChatsCount();
            return Ok(result);
        }
    }
}
