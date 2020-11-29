using Microsoft.AspNetCore.Mvc;
using MessageBoard.API.Entities;
using MessageBoard.API.Services.Interfaces;

namespace MessageBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpPost]
        public IActionResult Post(Client client)
        {
            var response = this.clientService.SetClient(client);
            return this.Ok(response);
        }
    }
}
