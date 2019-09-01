namespace TradingApp.Services
{
    using TradingApp.Interfaces;
    using TradingApp.Core.DTO;
    using TradingApp.Core.Interfaces;
    using System;

    class ConsoleApp : IConsoleApp
    {
        private readonly IInputOutputModule inputOutputModule;
        private readonly IClientService clientService;
        private readonly ISharesService sharesService;
        private readonly IPortfoliosService portfoliosService;

        public ConsoleApp(IInputOutputModule inputOutputModule, IClientService clientService, ISharesService sharesService, IPortfoliosService portfoliosService)
        {
            this.inputOutputModule = inputOutputModule;
            this.clientService = clientService;
            this.sharesService = sharesService;
            this.portfoliosService = portfoliosService;
        }

        public void RegisterClient()
        {
            ClientRegistrationData clientData = new ClientRegistrationData();

            inputOutputModule.WriteOutput("Write first name:");
            clientData.ClientName = inputOutputModule.ReadInput();

            inputOutputModule.WriteOutput("Write phone number:");
            clientData.ClientPhone = inputOutputModule.ReadInput();

            inputOutputModule.WriteOutput("Write balance:");
            while (true)
            {
                try
                {
                    clientData.Balance = Convert.ToDecimal(inputOutputModule.ReadInput());
                    break;
                }

                catch (FormatException)
                {
                    inputOutputModule.WriteOutput("Please enter valid balance.");
                }
            }

            clientService.RegisterClient(clientData);
        }

        public void RegisterShare()
        {
            ShareRegistrationData shareData = new ShareRegistrationData();

            inputOutputModule.WriteOutput("Write share type:");
            shareData.ShareType = inputOutputModule.ReadInput();

            inputOutputModule.WriteOutput("Write share price:");
            while (true)
            {
                try
                {
                    shareData.SharePrice = Convert.ToDecimal(inputOutputModule.ReadInput());
                    break;
                }

                catch (FormatException)
                {
                    inputOutputModule.WriteOutput("Please enter valid share price.");
                }
            }
            sharesService.RegisterShare(shareData);
        }

        public void RegisterPortfolio()
        {
            PortfolioData portfolioData = new PortfolioData();

            while (true)
            {
                inputOutputModule.WriteOutput("Write Stock Type:");
                string shareType = inputOutputModule.ReadInput();
                portfolioData.ShareID = sharesService.GetShareIDByType(shareType);

                if (portfolioData.ShareID != 0)
                {
                    break;
                }

                inputOutputModule.WriteOutput("Please enter valid share type.");
            }

            while (true)
            {
                inputOutputModule.WriteOutput("Write client name:");
                string clientName = inputOutputModule.ReadInput();
                portfolioData.ClientID = clientService.GetClientIDByName(clientName);

                if (portfolioData.ClientID != 0)
                {
                    break;
                }

                inputOutputModule.WriteOutput("Please enter valid name and surname.");
            }

            inputOutputModule.WriteOutput("Write amount of shares:");
            while (true)
            {
                try
                {
                    portfolioData.AmountOfShares = Convert.ToInt32(inputOutputModule.ReadInput());
                    break;
                }

                catch (FormatException)
                {
                    inputOutputModule.WriteOutput("Please enter valid amount of shares.");
                }
            }
            portfoliosService.RegisterPortfolio(portfolioData);
        }
    }
}
