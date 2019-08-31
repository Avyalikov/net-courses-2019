using Microsoft.AspNetCore.Mvc;
using System;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;
using TradingApp.Core.Services;
using TradingApp.Core.DTO;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("deal")]
    [ApiController]
    public class TransactionController : ControllerBase

    {
        private readonly IRepository<TransactionEntity> transactionRepository;
        private readonly TransactionService transactionService;

        public TransactionController(IRepository<TransactionEntity> transactionRepository, TransactionService transactionService)
        {
            this.transactionRepository = transactionRepository;
            this.transactionService = transactionService;
        }
        //[Route("get")]
        //[HttpGet]
        //public ActionResult<List<TraderEntity>> Get(int top, int page)
        //{
        //    return this.clientRepository.GetAll();
        //}
        [Route("make")]
        [HttpPost]
        public ActionResult MakeTransaction([FromBody] TransactionInfo transactionInfo)
        {
            try
            {
                transactionService.MakeShareTransaction(
                    transactionInfo.SellerId, 
                    transactionInfo.BuyerId, 
                    transactionInfo.ShareId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [Route("")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetTopTransactionsByUser(int clientId, int top)
        {
            try
            {
                return Ok(transactionService.GetTopTransactionsByUser(clientId, top));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
