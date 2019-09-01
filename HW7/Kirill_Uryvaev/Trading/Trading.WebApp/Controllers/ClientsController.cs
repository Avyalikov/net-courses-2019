using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Trading.Core;
using Trading.Core.DataTransferObjects;

namespace Trading.WebApp.Controllers
{
    public class ClientsController : ApiController
    {
        private readonly IClientService clientService;

        public ClientsController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        public HttpResponseMessage Get()
        {
            var parameters =HttpUtility.ParseQueryString(Request.RequestUri.Query);
            if (!parameters.AllKeys.Any(k => k == "top"))
            {
                return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} does not contains appropriate command"));
            }
            int top;
            if (!int.TryParse(parameters.Get("top"), out top))
                return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} top should be int"));
            if (top < 1)
                top = 1;
            int page = 1;
            if (!parameters.AllKeys.Any(k => k == "page"))
            {
                if(!int.TryParse(parameters.Get("page"), out page))
                    return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} page should be int"));
                if (page < 1)
                    page = 1;
            }
            var topClients = clientService.GetAllClients().OrderBy(x=>x.ClientID).Skip((page-1)* top).Take(top).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, topClients);
        }

        [ActionName("add")]
        public HttpResponseMessage Post([FromBody]ClientRegistrationInfo clientInfo)
        {
            int id = clientService.AddClient(clientInfo);            
            var newClient = clientService.GetAllClients().Where(x => x.ClientID == id).FirstOrDefault();
            return Request.CreateResponse(HttpStatusCode.OK, newClient);
        }

        [ActionName("update")]
        public HttpResponseMessage Post([FromBody]ClientEntity client)
        {
            clientService.UpdateClient(client);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ActionName("remove")]
        public HttpResponseMessage Post([FromBody]int id)
        {
            clientService.RemoveClient(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    
    }
}
