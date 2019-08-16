namespace Trading.Logic
{
    using System.Threading;
    using System.Threading.Tasks;
    using TradingData;
    using TradingView.Interface;

    public class Home
    {
        private readonly IView viewProvider = SettingsByLayers.viewProvider;
        private readonly IDbProvider dbProvider;
        private readonly UserLogic user;
        private readonly Transaction transaction;
        private readonly Stock stock;
        private static bool TradeStart = false;

        public Home(IView viewProvider, IDbProvider dbProvider)
        {
            this.viewProvider = viewProvider;
            this.dbProvider = dbProvider;
            user = new UserLogic(viewProvider, dbProvider);
            transaction = new Transaction(dbProvider);
            stock = new Stock(viewProvider, dbProvider);

        }

        private async void TradeAsync()
        {          
            while (TradeStart)
            {
                await Task.Run(() => transaction.Run());
                Thread.Sleep(100);
            }
        }       

        public void Run()
        {
            int UserSelect = viewProvider.IndexMain(TradeStart);
            switch (UserSelect)
            {
                case 1:
                    if (!TradeStart)
                    {
                        TradeStart = true;
                        TradeAsync();                        
                    }
                    else 
                        TradeStart = false;
                    break;
                case 2:
                    viewProvider.PrintAllUsers(user.ListUsers());
                    break;
                case 3:
                    user.AddUser();
                    break;
                case 4:
                    viewProvider.PrintAllStocks(stock.ListStocks());
                    break;
                case 5:
                    viewProvider.PrintAllStocks(stock.ListStocks());
                    stock.ChangeStockPrice();
                    break;
                case 6:
                    viewProvider.PrintOrangeZone(user.OrangeZone());
                    break;
                case 7:
                    viewProvider.PrintBlackZone(user.BlackZone());
                    break;
            }
        }
    }
}
