//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Trading.Core;

//namespace Trading.ClientApp
//{
//    class TradeSimulator
//    {
//        private readonly ILogger logger;
//        private readonly IValidator validator;
//        private readonly RequestSender requestSender;

//        private Random uniformRandomiser;

//        public TradeSimulator(IValidator validator, ILogger logger, RequestSender requestSender)
//        {
//            this.validator = validator;
//            this.logger = logger;
//            this.requestSender = requestSender;

//            logger.InitLogger();
//            uniformRandomiser = new Random();
//        }

//        public void ClientsTrade()
//        {
//            var clients = requestSender.GetTop10(1);
//            if (validator.ValidateClientList(clients, logger))
//            {
//                var tradingClients = clients.OrderBy(x => Guid.NewGuid()).Take(2).ToList();
//                if (validator.ValidateTradingClient(tradingClients[0], logger))
//                {
//                    logger.WriteInfo($"Starting operation between {tradingClients[0].ClientID} and {tradingClients[1].ClientID}");
//                    ClientsSharesEntity shareType = tradingClients[0].ClientsShares.Where(x => x.Amount > 0).OrderBy(x => Guid.NewGuid()).First();
//                    int numberOfSoldShares = uniformRandomiser.Next(1, (int)shareType.Amount);
//                    tradingOperationService.SellAndBuyShares(tradingClients[0].ClientID, tradingClients[1].ClientID, shareType, numberOfSoldShares);
//                    logger.WriteInfo($"Client {tradingClients[0].ClientID} sold {numberOfSoldShares} shares of {shareType.ShareID} to {tradingClients[1].ClientID}");
//                    logger.WriteInfo($"Operation successfully ended");
//                }
//            }
//        }
//    }
//}
