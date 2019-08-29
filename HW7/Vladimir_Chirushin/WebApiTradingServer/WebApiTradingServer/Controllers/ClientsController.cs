using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TradingSoftware.Core.Models;
using TradingSoftware.Core.Services;

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
        public ActionResult<IEnumerable<string>> Get()
        { 
            IEnumerable<Client> clients = clientManager.GetAllClients();
            List<string> answer = new List<string>();
            foreach (var client in clients)
            {
                answer.Add($"ClientID:{client.ClientID} Name: {client.Name} PhoneNumber: {client.PhoneNumber} Balance: {client.Balance}");
            }
            string[] answerString = answer.ToArray();
            return answerString;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] ValuesRequestData value)
        {
            return new ActionResult<string>("testResponse");
        }
    }
}
