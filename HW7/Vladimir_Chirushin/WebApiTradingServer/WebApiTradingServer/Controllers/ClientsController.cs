using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TradingSoftware.Core.Models;
using TradingSoftware.Core.Services;
using System.Linq;

namespace WebApiTradingServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientManager clientManager;

        public ClientsController(IClientManager clientManager)
        {
            this.clientManager = clientManager;
        }

        // GET api/clients
        [HttpGet]
        public ActionResult<IEnumerable<Client>> Get(int top, int page)
        {
            //IEnumerable<Client> clients =
            return Ok(clientManager.GetAllClients().Select(c => new { c.Name, c.Balance }).Skip((page - 1) * top).Take(top));

        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] ValuesRequestData value)
        {
            return new ActionResult<string>("testResponse");
        }
    }


    [Route("api/clients/[controller]")]
    [ApiController]
    public class AddController : ControllerBase
    {
        private readonly IClientManager clientManager;

        public AddController(IClientManager clientManager)
        {
            this.clientManager = clientManager;
        }


        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] Client client)
        {
            clientManager.AddClient(client);
            return new ActionResult<string>("testResponse");
        }

        // GET api/clients
        [HttpGet]
        public ActionResult<IEnumerable<Client>> Get(int top, int page)
        {
            //IEnumerable<Client> clients =
            return Ok(clientManager.GetAllClients().Select(c => new { c.Name, c.Balance }).Skip((page - 1) * top).Take(top));

        }
    }
}
