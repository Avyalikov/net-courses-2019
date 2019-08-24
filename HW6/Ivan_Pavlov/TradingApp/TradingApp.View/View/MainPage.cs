namespace TradingApp.View.View
{
    using StructureMap;
    using System;
    using System.Text;
    using System.Threading;
    using TradingApp.Core;
    using TradingApp.Core.ProxyForServices;
    using TradingApp.Core.Services;
    using TradingApp.Core.ServicesInterfaces;
    using TradingApp.View.Interface;

    class MainPage
    {
        private readonly IUsersService usersService;
        private readonly IShareServices shareServices;
        private readonly ITransactionServices transactionServices;
        private readonly IPortfolioServices portfolioServices;
        private readonly SimulatorOfTrading simulator;
        private readonly IIOProvider iOProvider;
        private readonly IPhraseProvider phraseProvider;
        private bool tradeStart = false;
        private Thread thread;

        public MainPage(Container container, IIOProvider iOProvider, IPhraseProvider phraseProvider)
        {           
            this.iOProvider = iOProvider;
            this.phraseProvider = phraseProvider;
            this.shareServices = container.GetInstance<ShareProxy>();
            this.usersService = container.GetInstance<UsersProxy>();
            this.transactionServices = container.GetInstance<TransactionProxy>();
            this.portfolioServices = container.GetInstance<PortfolioProxy>();
            this.simulator = container.GetInstance<SimulatorOfTrading>();
        }

        private void Transaction()
        {
            while (tradeStart)
            {
                simulator.StartTrading();
                Thread.Sleep(100);
            }
        }

        public void Run()
        {
            int UserSelect = SelectFeature();
            switch (UserSelect)
            {
                case 1:
                    if (!tradeStart)
                    {
                        tradeStart = true;
                        thread = new Thread(new ThreadStart(Transaction));
                        thread.Start();
                    }
                    else
                        tradeStart = false;
                    break;
                case 2:
                    new UserView(phraseProvider, iOProvider)
                        .PrinaAllUsers(usersService.GetAllUsers());
                    iOProvider.ReadKey();
                    break;
                case 3:
                    usersService.AddNewUser(new UserView(phraseProvider, iOProvider)
                        .CreateUser());
                    break;
                case 4:
                    new ShareView(phraseProvider, iOProvider)
                        .PrintAllShares(shareServices.GetAllShares());
                    iOProvider.WriteLine(phraseProvider.GetPhrase("BackToMain"));
                    iOProvider.ReadKey();
                    break;
                case 5:
                    ShareView share = new ShareView(phraseProvider, iOProvider);
                    share.PrintAllShares(shareServices.GetAllShares());
                    try
                    {
                        shareServices.ChangeSharePrice(share.ShareId(), share.ShareNewPrice());
                    }
                    catch (Exception ex)
                    {
                        iOProvider.WriteLine(ex.Message);
                    }
                    iOProvider.WriteLine(phraseProvider.GetPhrase("BackToMain"));
                    iOProvider.ReadKey();
                    break;
                case 6:
                    new UserView(phraseProvider, iOProvider)
                        .PrintAllUsersInOrange(usersService.GetAllUsersWithZero());
                    iOProvider.ReadKey();
                    break;
                case 7:
                    new UserView(phraseProvider, iOProvider)
                        .PrintAllUsersInBlack(usersService.GetAllUsersWithNegativeBalance());
                    iOProvider.ReadKey();
                    break;
            }
        }

        private int SelectFeature(bool inputError = false)
        {
            PrintFeature(inputError);
            if (int.TryParse(iOProvider.ReadLine(), out int UserSelect))
                return UserSelect;
            else
                return SelectFeature(true);
        }

        private void PrintFeature(bool inputError)
        {
            iOProvider.Clear();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(phraseProvider.GetPhrase("WelcomeMain"));
            if (!tradeStart)
                sb.AppendLine(string.Format("1. {0}", phraseProvider.GetPhrase("StartTrading")));
            else
                sb.AppendLine(string.Format("1. {0}", phraseProvider.GetPhrase("StopTrading")));
            sb.AppendLine(string.Format("2. {0}", phraseProvider.GetPhrase("UsersList")));
            sb.AppendLine(string.Format("3. {0}", phraseProvider.GetPhrase("CreateUser")));
            sb.AppendLine(string.Format("4. {0}", phraseProvider.GetPhrase("StocksList")));
            sb.AppendLine(string.Format("5. {0}", phraseProvider.GetPhrase("ChangeStockPrice")));
            sb.AppendLine(string.Format("6. {0}", phraseProvider.GetPhrase("OrangeZone")));
            sb.AppendLine(string.Format("7. {0}", phraseProvider.GetPhrase("BlackZone")));

            if (inputError)
                sb.AppendLine(phraseProvider.GetPhrase("InputError"));

            iOProvider.WriteLine(sb.ToString());
        }
    }
}
