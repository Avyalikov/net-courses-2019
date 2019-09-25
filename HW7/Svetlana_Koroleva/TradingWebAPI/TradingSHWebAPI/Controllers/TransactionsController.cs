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
    [Route("transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IOrderService orderService;

        public TransactionsController(IUnitOfWork unitOfWork, IOrderService service)
        {
            this.unitOfWork = unitOfWork;
            this.orderService = service;
        }

        //Get top transactions for client
        [HttpGet]
        public IActionResult GetTop(int clientId, int amount)
        {
            try
            {
                var transactions = this.orderService.GetTopOrdersForClient(amount, clientId);
                return Ok(transactions);
            }
            catch (Exception e)
            {
                var ex = e.Message;
                return StatusCode(500);

            }
        }

        [HttpGet]
        [Route("lastorder")]
        public  IActionResult GetLast()
        {
            try
            {
                var transaction = this.orderService.LastOrder();
                return Ok(transaction);
            }
            catch (Exception e)
            {
                var ex = e.Message;
                return StatusCode(500);

            }
        }


    }
}