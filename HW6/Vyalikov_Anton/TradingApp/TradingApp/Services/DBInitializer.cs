namespace TradingApp.Services
{
    using TradingApp.Core.Interfaces;
    using TradingApp.Core.DTO;
    using TradingApp.Core.Models;
    using TradingApp.Interfaces;
    using System;
    using System.Linq;

    class DBInitializer : IDBInitializer
    {
        private readonly IClientService clientService;
        private readonly ISharesService sharesService;
        private readonly IPortfoliosService portfoliosService;
        private Random random = new Random();

        public DBInitializer(IClientService clientService, ISharesService sharesService, IPortfoliosService portfoliosService)
        {
            this.clientService = clientService;
            this.sharesService = sharesService;
            this.portfoliosService = portfoliosService;
        }

        public void GenerateDB()
        {
            this.clientService.RegisterClient(new ClientRegistrationData { ClientName = "Ivanov Ivan", ClientPhone = "5553535", Balance = (decimal)45938.12 });
            this.clientService.RegisterClient(new ClientRegistrationData { ClientName = "Petrov Alex", ClientPhone = "3330214", Balance = (decimal)43709.14 });
            this.clientService.RegisterClient(new ClientRegistrationData { ClientName = "Kojima Hideo ", ClientPhone = "9391222", Balance = (decimal)2356079.45 });
            this.clientService.RegisterClient(new ClientRegistrationData { ClientName = "Durov Pavel ", ClientPhone = "5550243", Balance = (decimal)527803.39 });
            this.clientService.RegisterClient(new ClientRegistrationData { ClientName = "Gregson Harry", ClientPhone = "4930975", Balance = (decimal)9056387.26 });
            this.clientService.RegisterClient(new ClientRegistrationData { ClientName = "Payne Max", ClientPhone = "4931935", Balance = (decimal)9368.23 });
            this.clientService.RegisterClient(new ClientRegistrationData { ClientName = "Montana Tony", ClientPhone = "5550554", Balance = (decimal)43789.75 });
            this.clientService.RegisterClient(new ClientRegistrationData { ClientName = "Drake Nathan", ClientPhone = "7963246", Balance = (decimal)89358.93 });
            this.clientService.RegisterClient(new ClientRegistrationData { ClientName = "Sallivan Viktor", ClientPhone = "2344251", Balance = (decimal)438526.01 });
            this.clientService.RegisterClient(new ClientRegistrationData { ClientName = "Chloe Frazer", ClientPhone = "2645653", Balance = (decimal)463165.57 });


            this.sharesService.RegisterShare(new ShareRegistrationData { ShareType = "Xilinx", SharePrice = (decimal)104.23 });
            this.sharesService.RegisterShare(new ShareRegistrationData { ShareType = "Texas Instruments", SharePrice = (decimal)120.61 });
            this.sharesService.RegisterShare(new ShareRegistrationData { ShareType = "Boston Scientific Corp", SharePrice = (decimal)43.46 });
            this.sharesService.RegisterShare(new ShareRegistrationData { ShareType = "STMicroelectronics", SharePrice = (decimal)15.68 });
            this.sharesService.RegisterShare(new ShareRegistrationData { ShareType = "NXP Semiconductors", SharePrice = (decimal)99.88 });
            this.sharesService.RegisterShare(new ShareRegistrationData { ShareType = "Strandberg", SharePrice = (decimal)4.10 });
            this.sharesService.RegisterShare(new ShareRegistrationData { ShareType = "Intel", SharePrice = (decimal)45.98 });
            this.sharesService.RegisterShare(new ShareRegistrationData { ShareType = "Nvidia", SharePrice = (decimal)154.18 });
            this.sharesService.RegisterShare(new ShareRegistrationData { ShareType = "Gigabyte Technology", SharePrice = (decimal)46.80 });
            this.sharesService.RegisterShare(new ShareRegistrationData { ShareType = "Lockheed Martin", SharePrice = (decimal)377.18 });

            this.GeneratePortfolio();
        }

        public void CreateRandomShares()
        {
            int clientID = this.random.Next(1, this.clientService.GetAllClients().Count());
            int shareID = this.random.Next(1, this.clientService.GetAllClients().Count());
            int amountOfShares = this.random.Next(1, 10);

            var portfolio = new ClientPortfolio
            {
                ClientID = clientID,
                ShareID = shareID,
                AmountOfShares = amountOfShares
            };

            this.portfoliosService.ChangeAmountOfShares(portfolio);
        }

        public void GeneratePortfolio()
        {
            int numberOfShares = 200;
            for (int i = 0; i < numberOfShares; i++)
            {
                this.CreateRandomShares();
            }
        }
    }
}
