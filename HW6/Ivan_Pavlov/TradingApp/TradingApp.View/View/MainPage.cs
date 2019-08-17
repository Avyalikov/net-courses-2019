namespace TradingApp.View.View
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using TradingApp.View.Interface;
    using TradingApp.View.Logger;

    class MainPage
    {
        private readonly ITradeLogic logic;
        private readonly IIOProvider iOProvider;
        private readonly IPhraseProvider phraseProvider;
        private bool tradeStart = false;
        private Thread thread;

        public MainPage(ITradeLogic logic, IIOProvider iOProvider, IPhraseProvider phraseProvider)
        {
            this.logic = logic;
            this.iOProvider = iOProvider;
            this.phraseProvider = phraseProvider;                    
        }

        private void Transaction()
        {
            while (tradeStart)
            {
                try
                {
                    Logger.Log.Info(logic.TransactionRun());
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.Message);
                }
                Thread.Sleep(1);
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
                        .PrinaAllUsers(logic.ListUsers());
                    iOProvider.ReadKey();
                    break;
                case 3:
                    Logger.Log.Info(logic.AddUser(new UserView(phraseProvider, iOProvider)
                        .CreateUser()));
                    break;
                case 4:
                    new ShareView(phraseProvider, iOProvider)
                        .PrintAllShares(logic.ListStocks());
                    iOProvider.WriteLine(phraseProvider.GetPhrase("BackToMain"));
                    iOProvider.ReadKey();
                    break;
                case 5:
                    ShareView share = new ShareView(phraseProvider, iOProvider);
                    share.PrintAllShares(logic.ListStocks());
                    try
                    {
                        Logger.Log.Info(logic.ChangeStockPrice(share.ShareId(), share.ShareNewPrice()));
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error(ex);
                        iOProvider.WriteLine(ex.Message);
                    }
                    iOProvider.WriteLine(phraseProvider.GetPhrase("BackToMain"));
                    iOProvider.ReadKey();
                    break;
                case 6:
                    new UserView(phraseProvider, iOProvider)
                        .PrintAllUsersInOrange(logic.OrangeZone());
                    iOProvider.ReadKey();
                    break;
                case 7:
                    new UserView(phraseProvider, iOProvider)
                        .PrintAllUsersInBlack(logic.BlackZone());
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
