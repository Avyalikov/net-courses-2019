namespace WebApiTradingServer.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using TradingSoftware.Core.Dto;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Services;

    [Route("deal/[controller]")]
    [ApiController]
    public class MakeController : ControllerBase
    {
        private readonly ITransactionManager transactionManager;

        public MakeController(ITransactionManager transactionManager)
        {
            this.transactionManager = transactionManager;
        }

        // GET api/transactions?clientID=...
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get(int clientID)
        {
            IEnumerable<TransactionsFullData> transactions = transactionManager.GetTransactionWithClient(clientID);
            List<string> answer = new List<string>();
            foreach (var transaction in transactions)
            {
                answer.Add($"Seller:{transaction.SellerName} Buyer: {transaction.BuyerName} ShareID: {transaction.ShareType} SharePrice: {transaction.SharePrice}$ Amount: {transaction.ShareAmount}");
            }
            return answer.ToArray();
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post(int sellerID, int buyerID, int shareID, int shareAmount)
        {
            var transaction = new Transaction
            {
                SellerID = sellerID,
                BuyerID = buyerID,
                ShareID = shareID,
                Amount = shareAmount
            };

            if(transactionManager.Validate(transaction))
            {
                transactionManager.TransactionAgent(transaction);
                transactionManager.AddTransaction(transaction);
                return new ActionResult<string>("Success");
            }
            return new ActionResult<string>("Fail");
        }
    }
}
