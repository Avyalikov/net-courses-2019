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
        private readonly IClientService clientService;
        private readonly IShareService shareService;
        private readonly IClientsSharesService clientsSharesService;

        public TradingOperationService(IClientService clientService, IShareService shareService, IClientsSharesService clientsSharesService)
        {
            this.clientService = clientService;
            this.shareService = shareService;
            this.clientsSharesService = clientsSharesService;
        }

        public void SellAndBuyShares(int firstClientID, int secondClientID, ClientsSharesEntity shareType, int numberOfSoldShares)
        {
            if (numberOfSoldShares > shareType.Amount)
            {
                throw new ArgumentException($"Cannot sell {numberOfSoldShares} that more than client have {shareType.Amount}");
            }
            if (firstClientID == secondClientID)
            {
                throw new ArgumentException($"Cannot sell shares to yourself");
            }
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
