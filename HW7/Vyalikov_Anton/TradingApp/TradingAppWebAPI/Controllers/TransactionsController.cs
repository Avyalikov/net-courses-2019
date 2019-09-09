namespace TradingAppWebAPI.Controllers
{
    using TradingApp.Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class TransactionsController : ApiController
    {
        private readonly ITransactionsService transactionsService;

        public TransactionsController(ITransactionsService transactionsService)
        {
            this.transactionsService = transactionsService;
        }

        public HttpResponseMessage Get()
        {
            int top;
            int clientId;
            var parameters = System.Web.HttpUtility.ParseQueryString(Request.RequestUri.Query);
            IEnumerable<string> result = new List<string>();

            if (!parameters.AllKeys.Any(k => k == "clientId") && !parameters.AllKeys.Any(k => k == "top"))
            {
                return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} does not contains appropriate command"));
            }

            if (!int.TryParse(parameters.Get("clientId"), out clientId))
            {
                return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} clientId should be an integer."));
            }
                
            if (!int.TryParse(parameters.Get("top"), out top))
                return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} top should be an integer."));

            var clientShares = transactionsService.GetAllTransactions().Where(x => x.BuyerID == clientId).OrderBy(x => x.Date).Take(top);
            return Request.CreateResponse(HttpStatusCode.OK, clientShares);
        }
    }
}
