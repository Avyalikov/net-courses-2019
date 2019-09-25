namespace TradingApp.Services
{
    using TradingApp.Core.Interfaces;
    using TradingApp.Core.Models;
    using TradingApp.Core.Services;
    using TradingApp.Interfaces;
    using System;
    using System.Linq;

    class TradingSimulation : ITradingSimulation
    {
        private readonly ILogger logger;
        private readonly IValidationService validationService;
        private readonly ClientService clientService;
        private readonly SharesService sharesService;
        private readonly PortfoliosService portfoliosService;
        private readonly TransactionsService transactionsService;
        private Random random = new Random();

        public TradingSimulation(ILogger logger, IValidationService validationService, ClientService clientService,
                                  SharesService sharesService, PortfoliosService portfoliosService, TransactionsService transactionsService)
        {
            this.logger = logger;
            this.validationService = validationService;
            this.clientService = clientService;
            this.sharesService = sharesService;
            this.portfoliosService = portfoliosService;
            this.transactionsService = transactionsService;

        }

        public bool Trading()
        {
            int sharesForTrade = random.Next(1, 20);

            Transaction transaction = new Transaction
            {
                BuyerID = random.Next(1, clientService.GetAllClients().Count()),
                SellerID = random.Next(1, clientService.GetAllClients().Count()),
                ShareID = random.Next(1, sharesService.GetAllShares().Count()),
                AmountOfShares = sharesForTrade,
                Date = DateTime.Now,
            };

            if (validationService.ValidateTransaction(transaction, logger))
            {
                transactionsService.SellOrBuyShares(transaction);
                transactionsService.AddTransaction(transaction);
                return true;
            }
            return false;
        }
    }
}
