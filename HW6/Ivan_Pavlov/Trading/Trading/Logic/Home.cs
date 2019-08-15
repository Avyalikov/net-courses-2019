namespace Trading.Logic
{
    using System.Threading;
    using System.Threading.Tasks;
    using TradingView.Interface;

    public static class Home
    {
        private static readonly IView viewProvider = SettingsByLayers.viewProvider;
        private static bool TradeStart = false;

        private static async void TradeAsync()
        {
            while (TradeStart)
            {
                await Task.Run(() => Transaction.Run());
                Thread.Sleep(100);
            }
        }

        public static void Run()
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
                    viewProvider.PrintAllUsers(User.ListUsers());
                    break;
                case 3:
                    User.AddUser();
                    break;
                case 4:
                    viewProvider.PrintAllStocks(Stock.ListStocks());
                    break;
                case 5:
                    viewProvider.PrintAllStocks(Stock.ListStocks());
                    Stock.ChangeStockPrice();
                    break;
                case 6:
                    viewProvider.PrintOrangeZone(User.OrangeZone());
                    break;
                case 7:
                    viewProvider.PrintBlackZone(User.BlackZone());
                    break;
            }
        }
    }
}
