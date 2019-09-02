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

namespace TradingSHWebAPI.Controllers
{
    [Route("balances")]
    [Produces("application/json")]
    [ApiController]
    public class BalancesController : Controller

    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IClientService clientService;

        public BalancesController(IUnitOfWork unitOfWork, IClientService service)
        {
            this.unitOfWork = unitOfWork;
            this.clientService = service;
        }

        //Get client balance
        [HttpGet]
        public IActionResult GetBalace(int clientId)
        {
            try
            {
                var balance = this.clientService.GetClientBalance(clientId);
                var status = this.clientService.GetClientStatus(clientId);
                return Ok(balance);
            }
            catch (Exception e)
            {
                var ex = e.Message;
                return StatusCode(500);

            }
        }
    }
}