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
        private readonly IValidator validator;
        private readonly ILogger logger;

        private readonly ClientService clientService;
        private readonly ShareService shareService;
        private readonly ClientsSharesService clientsSharesService;

        private Random uniformRandomiser;

        public TradingOperationService(IValidator validator, ILogger logger, ClientService clientService, ShareService shareService, ClientsSharesService clientsSharesService)
        {
            this.validator = validator;
            this.clientService = clientService;
            this.shareService = shareService;
            this.clientsSharesService = clientsSharesService;
            this.logger = logger;

            uniformRandomiser = new Random();
            logger.InitLogger();
        }

        public void ClientsTrade()
        {
            logger.RunWithExceptionLogging(clientsTradeLogic);
        }

        private void clientsTradeLogic()
        {
            var clients = clientService.GetAllClients();
            if (validator.ValidateClientList(clients, logger))
            {
                var tradingClients = clients.OrderBy(x => Guid.NewGuid()).Take(2).ToList();
                if (validator.ValidateTradingClient(tradingClients[0], logger))
                {
                    ClientsSharesEntity shareType = tradingClients[0].ClientsShares.Where(x => x.Amount > 0).OrderBy(x => Guid.NewGuid()).First();
                    int numberOfSoldShares = uniformRandomiser.Next(1, (int)shareType.Amount);

                    exchangeShares(shareType, numberOfSoldShares, tradingClients[1].ClientID);
                    decimal shareCost = (decimal)shareService.GetAllShares().Where(x => x.ShareID == shareType.ShareID).Select(x => x.ShareCost).FirstOrDefault();

                    clientService.ChangeMoney(tradingClients[0].ClientID, shareCost * numberOfSoldShares);

                    clientService.ChangeMoney(tradingClients[1].ClientID, -(shareCost * numberOfSoldShares));

                    logger.WriteInfo($"Client {tradingClients[0].ClientID} sold {numberOfSoldShares} shares of {shareType.ShareID} to {tradingClients[1].ClientID}");
                }
            }
        }

        private void exchangeShares(ClientsSharesEntity shareType, int numberOfSoldShares, int secondClientID)
        {
            ClientsSharesInfo sharesInfo = new ClientsSharesInfo()
            {
                ClientID = shareType.ClientID,
                ShareID = shareType.ClientID,
                Amount = -numberOfSoldShares
            };
            clientsSharesService.ChangeClientsSharesAmount(sharesInfo);

            sharesInfo.Amount *= -1;
            sharesInfo.ClientID = secondClientID;

            clientsSharesService.ChangeClientsSharesAmount(sharesInfo);
        }
    }
}
