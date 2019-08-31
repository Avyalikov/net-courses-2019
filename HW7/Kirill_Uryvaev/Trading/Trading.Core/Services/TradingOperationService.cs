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
        private readonly TransactionHistoryService operationHistoryService;
        private readonly BalanceService balanceService;

        public TradingOperationService(IClientService clientService, IClientsSharesService clientsSharesService, TransactionHistoryService operationHistoryService, BalanceService balanceService)
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
                Amount = shareType.Amount - numberOfSoldShares,
                CostOfOneShare = shareType.CostOfOneShare
            };
            clientsSharesService.UpdateShares(sharesInfo);

            sharesInfo.ClientID = firstClientID;
            var secondClientShares = clientsSharesService.GetAllClientsShares().Where(x => x.ClientID == sharesInfo.ClientID && x.ShareID == sharesInfo.ShareID).FirstOrDefault();
            if (secondClientShares == null)
            {
                ClientsSharesInfo clientsSharesInfo = new ClientsSharesInfo()
                {
                    ClientID = sharesInfo.ClientID,
                    ShareID = sharesInfo.ShareID,
                    Amount = numberOfSoldShares
                };
                clientsSharesService.AddShares(clientsSharesInfo);
            }
            else
            {
                sharesInfo.Amount = secondClientShares.Amount + numberOfSoldShares;
                clientsSharesService.UpdateShares(sharesInfo);
            }

            balanceService.ChangeMoney(firstClientID, shareType.CostOfOneShare * numberOfSoldShares);
            balanceService.ChangeMoney(secondClientID, -(shareType.CostOfOneShare * numberOfSoldShares));

            TransactionHistoryInfo operationHistoryInfo = new TransactionHistoryInfo()
            {
                BuyerClientID = firstClientID,
                SellerClientID = secondClientID,
                ShareID = shareType.ShareID,
                Amount = numberOfSoldShares,
                SumOfOperation = shareType.CostOfOneShare * numberOfSoldShares,
                DateTime = DateTime.Now
            };

            operationHistoryService.Add(operationHistoryInfo);
        }
    }
}
