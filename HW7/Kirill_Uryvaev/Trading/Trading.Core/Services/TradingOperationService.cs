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
        private readonly IClientsSharesService clientsSharesService;
        private readonly OperationHistoryService operationHistoryService;
        private readonly BalanceService balanceService;

        public TradingOperationService(IClientService clientService, IClientsSharesService clientsSharesService, OperationHistoryService operationHistoryService, BalanceService balanceService)
        {
            this.clientService = clientService;
            this.clientsSharesService = clientsSharesService;
            this.operationHistoryService = operationHistoryService;
            this.balanceService = balanceService;
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
            ClientsSharesEntity sharesInfo = new ClientsSharesEntity()
            {
                ClientID = shareType.ClientID,
                ShareID = shareType.ShareID,
                Amount = -numberOfSoldShares
            };
            clientsSharesService.UpdateShares(sharesInfo);
            sharesInfo.Amount *= -1;
            sharesInfo.ClientID = secondClientID;
            clientsSharesService.UpdateShares(sharesInfo);

            balanceService.ChangeMoney(firstClientID, shareType.CostOfOneShare * numberOfSoldShares);
            balanceService.ChangeMoney(secondClientID, -(shareType.CostOfOneShare * numberOfSoldShares));

            OperationHistoryInfo operationHistoryInfo = new OperationHistoryInfo()
            {
                BuyerClientID = firstClientID,
                SellerClientID = secondClientID,
                ShareID = shareType.ShareID,
                Amount = numberOfSoldShares,
                SumOfOperation = shareType.CostOfOneShare * numberOfSoldShares
            };

            operationHistoryService.Add(operationHistoryInfo);
        }
    }
}
