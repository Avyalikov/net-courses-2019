using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;
using Trading.Core.DataTransferObjects;

namespace Trading.ClientApp
{
    class TradeSimulator
    {
        private readonly ILogger logger;
        private readonly RequestSender requestSender;

        private Random uniformRandomiser;

        public TradeSimulator(ILogger logger, RequestSender requestSender)
        {
            this.logger = logger;
            this.requestSender = requestSender;

            logger.InitLogger();
            uniformRandomiser = new Random();
        }

        public void ClientsTrade()
        {
            string answer = "";
            var clients = requestSender.GetTop10Clients(1,10,out answer);
            if (clients.Count() > 1)
            {
                var tradingClients = clients.OrderBy(x => Guid.NewGuid()).Take(2).ToList();
                logger.WriteInfo($"Starting operation between {tradingClients[0].ClientID} and {tradingClients[1].ClientID}");
                ClientsSharesEntity shareType = tradingClients[0].ClientsShares.Where(x => x.Amount > 0).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                if (shareType == null)
                {
                    logger.WriteWarn($"{tradingClients[0].ClientID} not have shares");
                    return;
                }
                int numberOfSoldShares = uniformRandomiser.Next(1, (int)shareType.Amount);
                TransactionHistoryInfo transaction = new TransactionHistoryInfo()
                {
                    BuyerClientID = tradingClients[1].ClientID,
                    SellerClientID = tradingClients[0].ClientID,
                    ShareID = shareType.ShareID,
                    Amount = numberOfSoldShares
                };
                requestSender.PostMakeDeal(transaction, out answer);
                logger.WriteInfo($"Result: {answer}");
            }
        }
    }
}
