using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Trading.Core;
using Trading.Core.DTO;
using Trading.Core.IServices;

namespace TradingSHWebAPI.Controllers
{
    [Route("deal")]
    [ApiController]
    public class DealController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IOrderService orderService;
        private readonly ITransactionHistoryService transactionService;

        public DealController(IUnitOfWork unitOfWork, IOrderService service, ITransactionHistoryService transactionService)
        {
            this.unitOfWork = unitOfWork;
            this.orderService = service;
            this.transactionService = transactionService;
        }


       [HttpPost]
       [Route("addorder")]
       public IActionResult Make([FromBody]OrderInfo orderInfo)
       {
           try
           {
                
               this.orderService.AddOrder(orderInfo);
               return Ok();
           }
           catch (Exception e)
           {
               var ex = e.Message;
               return StatusCode(500);

           }
       }

    


          [HttpPost]
          [Route("make")]
          public IActionResult Make(int orderId, [FromBody]TransactionInfo transactionInfo)
          {
              try
              {
                  this.transactionService.AddTransactionInfo(transactionInfo);
                  int transactionId = this.transactionService.GetLastTransaction().TransactionHistoryID;
                 this.orderService.SetIsExecuted(orderId, transactionId);
                  return Ok();
              }
              catch (Exception e)
              {
                  var ex = e.Message;
                  return StatusCode(500);

              }
          }
          

    }
}