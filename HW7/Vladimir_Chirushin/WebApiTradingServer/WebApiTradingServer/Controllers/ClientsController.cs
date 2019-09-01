namespace WebApiTradingServer.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Services;

    [Route("clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientManager clientManager;

        public ClientsController(IClientManager clientManager)
        {
            this.clientManager = clientManager;
        }

        // GET clients
        [HttpGet]
        public ActionResult<IEnumerable<Client>> GetClients(int top, int page)
        {
            return Ok(this.clientManager.GetAllClients().Select(c => new { c.ClientID, c.Name, c.PhoneNumber, c.Balance }).Skip((page - 1) * top).Take(top));
        }

        // POST /clients/add
        [HttpPost]
        [Route("add")]
        public ActionResult<string> AddClient([FromBody] Client client)
        {
            this.clientManager.AddClient(client);
            return Ok($"Client {client.Name} added");
        }

        // POST clients/update
        [HttpPost]
        [Route("update")]
        public ActionResult<string> UpdateClient([FromBody] Client client)
        {
            this.clientManager.ClientUpdate(client);
            return Ok($"Client {client.ClientID} updated");
        }

        // POST clients/add
        [HttpPost]
        [Route("remove")]
        public ActionResult<string> Post([FromBody] Client client)
        {
            this.clientManager.DeleteClient(client);
            return Ok($"Client {client.ClientID} removed");
        }
    }
}
