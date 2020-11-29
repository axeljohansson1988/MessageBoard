using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MessageBoard.API.Entities;
using MessageBoard.API.Services.Interfaces;

namespace MessageBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardMessageController : ControllerBase
    {
        private readonly IBoardMessageService boardMessageService;

        public BoardMessageController(IBoardMessageService boardMessageService)
        {
            this.boardMessageService = boardMessageService;
        }

        [HttpGet("{clientId}/{latestFirst}")]
        public IActionResult Get(int? clientId, bool latestFirst = true)
        {
            var allMessages = this.boardMessageService.GetBoardMessages(clientId, latestFirst);
            return this.Ok(allMessages);
        }

        [HttpPost]
        public IActionResult Post(BoardMessage boardMessage)
        {
            var response = this.boardMessageService.SetBoardMessage(boardMessage);
            return this.Ok(response);
        }

        [HttpPut]
        public IActionResult Put(BoardMessage boardMessage)
        {
            var response = this.boardMessageService.UpdateBoardMessage(boardMessage);
            return this.Ok(response);
        }

        [HttpDelete("{boardMessageId}/{clientId}")]
        public IActionResult Delete(int boardMessageId, int clientId)
        {
            var response = this.boardMessageService.DeleteBoardMessage(boardMessageId, clientId);
            return this.Ok(response);
        }
    }
}
