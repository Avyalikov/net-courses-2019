using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stockSimulator.Core.Models;
using stockSimulator.Core.Services;

namespace stockSimulator.WebServ.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ClientService clientService;

        public ClientsController(ClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<ClientEntity>> Get(int top, int page)
        {
            try
            {
                var clients = this.clientService.GetClients(top, page).ToList();
                return Ok(clients);
            }catch(Exeption ex)
            {
                return StatusCode(500, ex);
            }

        }

        [Serializable]
        private class Exeption : Exception
        {
            public Exeption()
            {
            }

            public Exeption(string message) : base(message)
            {
            }

            public Exeption(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected Exeption(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}