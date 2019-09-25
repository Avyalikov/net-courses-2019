namespace TradingAppWebAPI.Controllers
{
    using TradingApp.Core.DTO;
    using TradingApp.Core.Interfaces;
    using TradingApp.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;

    public class PortfoliosController : ApiController
    {
        private readonly IPortfoliosService portfoliosService;

        public PortfoliosController(IPortfoliosService portfoliosService)
        {
            this.portfoliosService = portfoliosService;
        }

        public HttpResponseMessage Get()
        {
            int clientId;
            var parameters = System.Web.HttpUtility.ParseQueryString(Request.RequestUri.Query);
            IEnumerable<string> result = new List<string>();

            if (!parameters.AllKeys.Any(x => x == "clientId"))
            {
                return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} does not contains required command."));
            }
            
            if (!int.TryParse(parameters.Get("clientId"), out clientId))
            {
                return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} clientId should be an integer."));
            }
            var clientShares = portfoliosService.GetAllPortfolios().Where(x => x.ClientID == clientId).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, clientShares);
        }

        [ActionName("add")]
        public HttpResponseMessage Post([FromBody]PortfolioData portfolioData)
        {
            portfoliosService.RegisterPortfolio(portfolioData);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ActionName("update")]
        public HttpResponseMessage Post([FromBody]ClientPortfolio portfolio)
        {
            portfoliosService.Update(portfolio);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("remove")]
        public HttpResponseMessage DeletePortfolio([FromBody] ClientPortfolio portfolio)
        {
            this.portfoliosService.Remove(portfolio);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
