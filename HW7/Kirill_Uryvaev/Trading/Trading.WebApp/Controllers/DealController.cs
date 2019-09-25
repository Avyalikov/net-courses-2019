using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Trading.Core.Services;
using Trading.Core.DataTransferObjects;

namespace Trading.WebApp.Controllers
{
    public class DealController : ApiController
    {
        private readonly TradingOperationService tradingOperationService;
        private readonly ClientsSharesService clientsSharesService;

        public DealController(TradingOperationService tradingOperationService, ClientsSharesService clientsSharesService)
        {
            this.tradingOperationService = tradingOperationService;
            this.clientsSharesService = clientsSharesService;
        }

        [ActionName("make")]
        public HttpResponseMessage Post([FromBody]TransactionHistoryInfo transactionInfo)
        {
            if(transactionInfo==null)
                return Request.CreateResponse(new ArgumentException($"Transaction information is empty"));
            var sharesService = clientsSharesService.GetAllClientsShares().Where(
                x => x.ClientID == transactionInfo.SellerClientID && x.ShareID == transactionInfo.ShareID).FirstOrDefault();
            if (sharesService == null)
            {
                return Request.CreateResponse(new ArgumentException($"Combination of client {transactionInfo.SellerClientID} and {transactionInfo.ShareID} not exist"));
            }
            tradingOperationService.SellAndBuyShares(transactionInfo.BuyerClientID, transactionInfo.SellerClientID, sharesService, transactionInfo.Amount);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
