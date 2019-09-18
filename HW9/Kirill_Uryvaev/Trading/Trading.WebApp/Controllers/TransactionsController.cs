using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Trading.Core.Services;

namespace Trading.WebApp.Controllers
{
    public class TransactionsController : ApiController
    {
        private readonly TransactionHistoryService transactionHistoryService;

        public TransactionsController(TransactionHistoryService transactionHistoryService)
        {
            this.transactionHistoryService = transactionHistoryService;
        }
        public HttpResponseMessage Get()
        {
            var parameters = System.Web.HttpUtility.ParseQueryString(Request.RequestUri.Query);
            IEnumerable<string> result = new List<string>();
            if (!parameters.AllKeys.Any(k => k == "clientId") && !parameters.AllKeys.Any(k => k == "top"))
            {
                return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} does not contains appropriate command"));
            }
            int clientId;
            if (!int.TryParse(parameters.Get("clientId"), out clientId))
                return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} clientId should be int"));
            int top;
            if (!int.TryParse(parameters.Get("top"), out top))
                return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} top should be int"));

            var clientShares = transactionHistoryService.GetOperationOfClient(clientId).OrderBy(x=>x.DateTime).Take(top);
            return Request.CreateResponse(HttpStatusCode.OK, clientShares);
        }
    }
}
