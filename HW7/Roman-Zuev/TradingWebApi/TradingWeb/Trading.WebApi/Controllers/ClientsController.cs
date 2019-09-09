using Microsoft.AspNetCore.Mvc;
using System;
using Trading.Core.Services;
using Trading.Core.Dto;

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
        // GET clients?top=int&page=int
        [HttpGet]
        public ActionResult<string> GetTop(int top, int page)
        {
            try
            {
                string clients = string.Empty;
                foreach (var item in clientsService.GetTop(top, page))
                {
                    clients += $"{item.Name}{Environment.NewLine}";
                }
                return clients;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // POST clients/add
        [HttpPost("[action]")]
        public ActionResult<string> Add([FromBody] ClientRegistrationInfo client)
        {
            try
            {
                clientsService.RegisterNew(client);
                return Ok("Client Successfully Registered");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST clients/update/id
        [HttpPost("[action]/{clientId}")]
        public ActionResult<string> Update(int clientId, [FromBody] ClientRegistrationInfo infoToUpdate)
        {
            try
            {
                clientsService.UpdateInfo(clientId, infoToUpdate);
                return Ok("Client Successfully Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST clients/Remove/id
        [HttpPost("[action]/{clientId}")]
        public ActionResult<string> Remove(int clientId)
        {
            try
            {
                clientsService.RemoveById(clientId);
                return Ok("Client Successfully Removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
