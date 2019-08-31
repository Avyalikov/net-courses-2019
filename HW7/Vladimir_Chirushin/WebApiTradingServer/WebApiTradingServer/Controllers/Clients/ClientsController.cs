using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TradingSoftware.Core.Models;
using TradingSoftware.Core.Services;
using System.Linq;

namespace WebApiTradingServer.Controllers.Clients
{
    [Route("[controller]")]
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
        public ActionResult<IEnumerable<Client>> Get(int top, int page)
        {
            return Ok(clientManager.GetAllClients().Select(c => new { c.ClientID, c.Name, c.Balance }).Skip((page - 1) * top).Take(top));
        }
    }

    [Route("clients/[Controller]")]
    [ApiController]
    public class AddController : ControllerBase
    {
        private readonly IClientManager clientManager;

        public AddController(IClientManager clientManager)
        {
            this.clientManager = clientManager;
        }


        // POST /clients/add
        [HttpPost]
        public ActionResult<string> Post([FromBody] Client client)
        {
            clientManager.AddClient(client);
            return new ActionResult<string>("Success");
        }
    }

    [Route("clients/[Controller]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private readonly IClientManager clientManager;

        public UpdateController(IClientManager clientManager)
        {
            this.clientManager = clientManager;
        }


        // POST clients/add
        [HttpPost]
        public ActionResult<string> Post([FromBody] Client client)
        {
            clientManager.ClientUpdate(client);
            return new ActionResult<string>("Success");
        }
    }

    [Route("clients/[Controller]")]
    [ApiController]
    public class RemoveController : ControllerBase
    {
        private readonly IClientManager clientManager;

        public RemoveController(IClientManager clientManager)
        {
            this.clientManager = clientManager;
        }


        // POST clients/add
        [HttpPost]
        public ActionResult<string> Post([FromBody] Client client)
        {
            clientManager.DeleteClient(client);
            return new ActionResult<string>("Success");
        }
    }

}
