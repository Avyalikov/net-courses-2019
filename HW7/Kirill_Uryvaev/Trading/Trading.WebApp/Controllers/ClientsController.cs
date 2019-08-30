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

        // GET api/values 
        public HttpResponseMessage Get()
        {
            var parameters =HttpUtility.ParseQueryString(Request.RequestUri.Query);
            IEnumerable<string> result = new List<string>();
            if (parameters.AllKeys.Any(k => k == "top10"))
            {
                return Request.CreateResponse(new ArgumentException("${Request.RequestUri.Query} does not contains appropriate command"));
            }
            int page = 1;
            if (parameters.AllKeys.Any(k => k == "page"))
            {
                if(!int.TryParse(parameters.Get("page"), out page))
                    return Request.CreateResponse(new ArgumentException("${Request.RequestUri.Query} page should be int"));
                if (page < 1)
                    page = 1;
            }
            var top10Clients = clientService.GetAllClients().OrderBy(x=>x.ClientBalance).Skip((page-1)*10).Take(10).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, top10Clients);
        }

        [Route("[controller]/add")]
        public HttpResponseMessage Post([FromBody]ClientRegistrationInfo clientInfo)
        {
            int id = clientService.AddClient(clientInfo);
            var newClient = clientService.GetAllClients().Where(x => x.ClientID == id).FirstOrDefault();
            return Request.CreateResponse(HttpStatusCode.OK, newClient);
        }

        [Route("[controller]/update")]
        public HttpResponseMessage Post([FromBody]ClientEntity client)
        {
            clientService.UpdateClient(client);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("[controller]/remove")]
        public HttpResponseMessage Post([FromBody]int id)
        {
            clientService.RemoveClient(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    
    }
}
