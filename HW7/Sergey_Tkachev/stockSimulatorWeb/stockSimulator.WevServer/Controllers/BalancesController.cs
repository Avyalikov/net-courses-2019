using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using stockSimulator.Core.DTO;
using stockSimulator.Core.Models;
using stockSimulator.Core.Services;

namespace stockSimulator.WevServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BalancesController : ControllerBase
    {
        private readonly ClientService clientService;

        public BalancesController(ClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        [Route("")]
        // clients
        public ActionResult<IEnumerable<ClientEntity>> GetStateOfClient(int clientId)
        {
            try
            {
                var zoneOfClient = this.clientService.GetStateOfClient(clientId);
                return Ok(zoneOfClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
