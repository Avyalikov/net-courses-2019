using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;
using Trading.Core.Services;

namespace Trading.ConsoleApp
{
    public class TradeSimulator
    {
        private readonly ILogger logger;
        private readonly IValidator validator;
        private readonly ClientService clientService;
        private readonly ShareService shareService;
        private readonly TradingOperationService tradingOperationService;

        private Random uniformRandomiser;

        public TradeSimulator(IValidator validator, ILogger logger, ClientService clientService, ShareService shareService, TradingOperationService tradingOperationService)
        {
            this.clientService = clientService;
            this.shareService = shareService;
            this.validator = validator;
            this.logger = logger;
            this.tradingOperationService = tradingOperationService;

            logger.InitLogger();
            uniformRandomiser = new Random();
        }

        public void ClientsTrade()
        {
            var clients = clientService.GetAllClients();
            if (validator.ValidateClientList(clients, logger))
            {
                var tradingClients = clients.OrderBy(x => Guid.NewGuid()).Take(2).ToList();
                if (validator.ValidateTradingClient(tradingClients[0], logger))
                {
                    logger.WriteInfo($"Starting operation between {tradingClients[0].ClientID} and {tradingClients[1].ClientID}");
                    ClientsSharesEntity shareType = tradingClients[0].ClientsShares.Where(x => x.Amount > 0).OrderBy(x => Guid.NewGuid()).First();
                    int numberOfSoldShares = uniformRandomiser.Next(1, (int)shareType.Amount);
                    tradingOperationService.SellAndBuyShares(tradingClients[0].ClientID, tradingClients[1].ClientID, shareType, numberOfSoldShares);
                    logger.WriteInfo($"Client {tradingClients[0].ClientID} sold {numberOfSoldShares} shares of {shareType.ShareID} to {tradingClients[1].ClientID}");
                    logger.WriteInfo($"Operation successfully ended");
                }
            }
        }
    }
}
