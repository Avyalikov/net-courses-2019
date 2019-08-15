namespace Trading.Logic
{
    using Trading.View;

    public static class Home
    {
        public static void Run()
        {
            int UserSelect = MainView.IndexMain();
            switch (UserSelect)
            {
                case 1:
                    Transaction.Run();
                    break;
                case 2:
                    PrintInfo.PrintAllUsers(User.ListUsers());
                    break;
                case 3:
                    User.AddUser();
                    break;
                case 4:
                    PrintInfo.PrintAllStocks(Stock.ListStocks());
                    break;
                case 5:
                    PrintInfo.PrintAllStocks(Stock.ListStocks());
                    Stock.ChangeStockPrice();
                    break;
                case 6:
                    PrintInfo.PrintOrangeZone(User.Zone(0));
                    break;
                case 7:
                    PrintInfo.PrintBlackZone(User.Zone(1));
                    break;

            }
        }
    }
}
