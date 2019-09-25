namespace TradingAppWebAPI.Controllers
{
    using TradingApp.Core.DTO;
    using TradingApp.Core.Interfaces;
    using TradingApp.Core.Models;
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;

    public class ClientsController : ApiController
    {
        private readonly IClientService clientService;

        public ClientsController(IClientService clientService)
        {
            this.clientService = clientService;
        }
        
        public HttpResponseMessage Get()
        {
            int top = 0;
            int page = 1;
            var topClients = clientService.GetAllClients().OrderBy(x => x.ClientID).Skip((page - 1) * top).Take(top).ToList();
            var parameters = HttpUtility.ParseQueryString(Request.RequestUri.Query);

            if (!parameters.AllKeys.Any(x => x == "top"))
            {
                return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} does not contains appropriate command"));
            }

            
            if (!int.TryParse(parameters.Get("top"), out top))
            {
                return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} top should be an integer."));
            }  

            if (top < 1)
            {
                top = 1;
            }
                
            if (!parameters.AllKeys.Any(x => x == "page"))
            {
                if (!int.TryParse(parameters.Get("page"), out page))
                    return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} page should be an integer."));
                if (page < 1)
                    page = 1;
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, topClients);
        }


        [ActionName("add")]
        public HttpResponseMessage Post([FromBody]ClientRegistrationData clientData)
        {
            clientService.RegisterClient(clientData);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ActionName("update")]
        public HttpResponseMessage Post([FromBody]Client client)
        {
            clientService.UpdateClient(client);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ActionName("remove")]
        public HttpResponseMessage Post([FromBody]int clientID)
        {
            clientService.RemoveClient(clientID);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
