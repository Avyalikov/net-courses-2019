using Microsoft.AspNet.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Trading.Core;
using Trading.Core.DataTransferObjects;
using Trading.Core.Services;

namespace Trading.WebApp.ODataControllers
{
    public class OSharesController : ODataController
    {
        private readonly ClientsSharesService shareService;
        public OSharesController(ClientsSharesService shareService)
        {
            this.shareService = shareService;
        }

        [EnableQuery]
        public IQueryable<ClientsSharesEntity> Get()
        {
            return shareService.GetAllClientsShares();
        }

        public async Task<IHttpActionResult> Post(ClientsSharesEntity share)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ClientsSharesInfo sharesInfo = new ClientsSharesInfo() { Amount = share.Amount, ClientID = share.ClientID, CostOfOneShare = share.CostOfOneShare, ShareID = share.ShareID };
            shareService.AddShares(sharesInfo);
            return Created(share);
        }
    }
}
