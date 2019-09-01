using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using Trading.Core.Services;

namespace Trading.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionHistoryService transactionHistoryService;

        public TransactionsController(ITransactionHistoryService transactionHistoryService)
        {
            this.transactionHistoryService = transactionHistoryService;
        }

        [HttpGet]
        public ActionResult<string> Get(int clientId, int top)
        {
            try
            {
                string transactions = string.Empty;
                foreach (var item in transactionHistoryService.GetTopByClientId(clientId, top))
                {
                    transactions += $"{item.DateTime} {item.Seller.Name} {item.Buyer.Name} " +
                        $"{item.SelledItem.SharesType} {item.Quantity}{Environment.NewLine}";
                }
                return transactions;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
