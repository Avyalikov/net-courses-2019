namespace TradingApp.OwinHostApi.Controller
{
    using System.Collections.Generic;
    using System.Web.Http;
    using TradingApp.Core.Models;
    using TradingApp.Core.ServicesInterfaces;

    public class TransactionController : ApiController
    {
        private readonly ITransactionServices transactionServices;

        public TransactionController(ITransactionServices transaction)
        {
            this.transactionServices = transaction;
        }

        public IHttpActionResult GetUsersTransactions(int userId, int top)
        {
            var transactions = transactionServices.GetUsersTransaction(userId);
            if (transactions == null)
                return null;
            else if (top > transactions.Count)
                return Json(transactions);
            else
                return Json(transactions.GetRange(0, top));
        }
    }
}
