using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Models;
using Trading.Core.Services;
using Trading.Repository.Context;
using Newtonsoft.Json;

namespace Trading.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService clientsService;

        public ClientsController(IClientsService clientsService)
        {
            this.clientsService = clientsService;
        }
        // GET api/values
        [HttpGet]
        public string Get(int top, int page)
        {

            return JsonConvert.SerializeObject(clientsService.GetClientsList(top, page));
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
    }
}
