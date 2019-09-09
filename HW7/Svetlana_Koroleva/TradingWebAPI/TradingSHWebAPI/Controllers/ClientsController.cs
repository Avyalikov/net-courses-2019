using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trading.Core.Services;
using Trading.Core.IServices;
using SharedContext;
using Trading.Core;
using Trading.Core.DTO;
using Trading.Core.Model;
using Microsoft.AspNetCore.Http;
using SharedContext.DAL;
using Newtonsoft.Json;



namespace TradingSHWebAPI
{
    [Route("clients")]
    [Produces("application/json")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IClientService clientService;

        public ClientController(IUnitOfWork unitOfWork, IClientService service)
        {
            this.unitOfWork = unitOfWork;
            this.clientService = service;
        }

        //Get top clients
        [HttpGet]
        public IActionResult GetTop(int top, int page)
        {
            try
            {
                var clients = this.clientService.GetTopClients(top);
                return Ok(clients);
            }
            catch (Exception e)
            {
                var ex = e.Message;
                return StatusCode(500);

            }
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody]ClientInfo client)
        {
            try
            {
                if (client == null)
                {
                    return BadRequest();
                }

                this.clientService.AddClientToDB(client);
                return Ok(client);
            }
            catch (Exception e)
            {
                var ex = e.Message;
                return StatusCode(500);

            }
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update(int id, [FromBody] ClientInfo client)
        {
            try
            {
                this.clientService.Update(id, client);
                return Ok();
            }
            catch (Exception e)
            {
                var ex = e.Message;
                return StatusCode(500);

            }
        }

        [HttpPost]
        [Route("remove")]
        public IActionResult Remove(int id)
        {
            try
            {
                this.clientService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                var ex = e.Message;
                return StatusCode(500);

            }
        }

        [HttpGet]
        [Route("getbyid")]
        public IActionResult GetClient(int id)
        {
            try
            {
                var client = this.clientService.GetEntityByID(id);
                return Ok(client);
            }
            catch (Exception e)
            {
                var ex = e.Message;
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("all")]

        public IActionResult GetAllClients()
        {
            try
            {
                var clients = this.clientService.GetAllClients();
                return Ok(clients);
            }
            catch (Exception e)
            {
                var ex = e.Message;
                return StatusCode(500);
            }
        }

    }
}
