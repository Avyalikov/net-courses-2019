namespace TradingAppWebAPI.Controllers
{
    using TradingApp.Core.Interfaces;
    using TradingApp.Core.Models;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class DealController : ApiController
    {
        private readonly ITransactionsService transactionsService;
        
        public DealController(ITransactionsService transactionsService)
        {
            this.transactionsService = transactionsService;
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Transaction transaction)
        {
            try
            {
                this.transactionsService.SellOrBuyShares(transaction);
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            catch (ArgumentException)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
