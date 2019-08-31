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
    public class SharesController : ApiController
    {
        private readonly IClientsSharesService shareService;

        public SharesController(IClientsSharesService shareService)
        {
            this.shareService = shareService;
        }
        public HttpResponseMessage Get()
        {
            var parameters = System.Web.HttpUtility.ParseQueryString(Request.RequestUri.Query);
            IEnumerable<string> result = new List<string>();
            if (!parameters.AllKeys.Any(k => k == "clientId"))
            {
                return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} does not contains appropriate command"));
            }
            int clientId ;
            if (!int.TryParse(parameters.Get("clientId"), out clientId))
                return Request.CreateResponse(new ArgumentException($"{Request.RequestUri.Query} clientId should be int"));
            var clientShares = shareService.GetAllClientsShares().Where(x=>x.ClientID==clientId).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, clientShares);
        }
        [ActionName("add")]
        public HttpResponseMessage Post([FromBody]ClientsSharesInfo sharesInfo)
        {
            shareService.AddShares(sharesInfo);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ActionName("update")]
        public HttpResponseMessage Post([FromBody]ClientsSharesEntity share)
        {
            shareService.UpdateShares(share);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ActionName("remove")]
        public HttpResponseMessage Post([FromBody]int[] id)
        {
            ClientsSharesEntity shareInfo = new ClientsSharesEntity()
            {
                ClientID=id[0],
                ShareID = id[1]
            };
            shareService.RemoveShares(shareInfo);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
