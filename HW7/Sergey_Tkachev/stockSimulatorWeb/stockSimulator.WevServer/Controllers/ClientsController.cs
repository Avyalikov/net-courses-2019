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
    public class ClientsController : ControllerBase
    {
        private readonly ClientService clientService;

        public ClientsController(ClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        [Route("")]
        // clients
        public ActionResult<IEnumerable<ClientEntity>> Get(int top, int page)
        {
            try
            {
                var clients = this.clientService.GetClients(top, page);
                return Ok(clients);
            }catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("add")]
        public ActionResult<string> AddNewClient([FromBody]ClientRegistrationInfo registrationInfo)
        {
            try
            {
                //ClientRegistrationInfo clientToAdd = JsonConvert.DeserializeObject<ClientRegistrationInfo>(registrationInfo);
                int registeredID = this.clientService.RegisterNewClient(registrationInfo);
                return Ok(registeredID);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("update")]
        public ActionResult<string> UpdateClient([FromBody]UpdateClientInfo updateInfo)
        {
            try
            {
                string result = this.clientService.UpdateClient(updateInfo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("remove")]
        public ActionResult<string> RemoveClient([FromBody]int clientId)
        {
            try
            {
                string result = this.clientService.RemoveClient(clientId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        
    }
}