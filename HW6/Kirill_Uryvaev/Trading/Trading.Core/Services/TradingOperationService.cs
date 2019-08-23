using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.DataTransferObjects;

namespace Trading.Core.Services
{
    public class TradingOperationService
    {
        private readonly ClientService clientService;
        private readonly ShareService shareService;
        private readonly ClientsSharesService clientsSharesService;

        public TradingOperationService(ClientService clientService, ShareService shareService, ClientsSharesService clientsSharesService)
        {
            this.clientService = clientService;
            this.shareService = shareService;
            this.clientsSharesService = clientsSharesService;
        }

        public void SellAndBuyShares(int firstClientID, int secondClientID, ClientsSharesEntity shareType, int numberOfSoldShares)
        {
            ClientsSharesInfo sharesInfo = new ClientsSharesInfo()
            {
                ClientID = shareType.ClientID,
                ShareID = shareType.ShareID,
                Amount = -numberOfSoldShares
            };
            clientsSharesService.ChangeClientsSharesAmount(sharesInfo);
            sharesInfo.Amount *= -1;
            sharesInfo.ClientID = secondClientID;
            clientsSharesService.ChangeClientsSharesAmount(sharesInfo);

            decimal shareCost = (decimal)shareService.GetAllShares().Where(x => x.ShareID == shareType.ShareID).Select(x => x.ShareCost).FirstOrDefault();
            clientService.ChangeMoney(firstClientID, shareCost * numberOfSoldShares);
            clientService.ChangeMoney(secondClientID, -(shareCost * numberOfSoldShares));
        }
    }
}
