using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Trading.Core.Services;

namespace Trading.WebApp.Controllers
{
    public class BalancesController : ApiController
    {
        private readonly BalanceService balanceService;

        public BalancesController(BalanceService balanceService)
        {
            this.balanceService = balanceService;
        }

        public HttpResponseMessage Get()
        {
            var parameters = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            IEnumerable<string> result = new List<string>();
            if (!parameters.AllKeys.Any(k => k == "clientId"))
            {
                return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} does not contains appropriate command"));
            }
            int clientId;
            if (!int.TryParse(parameters.Get("clientId"), out clientId))
                return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} clientId should be int"));
            return Request.CreateResponse(HttpStatusCode.OK, balanceService.GetBalanceZone(clientId));
        }
    }
}
