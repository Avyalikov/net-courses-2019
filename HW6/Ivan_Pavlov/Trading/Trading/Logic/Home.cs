using Trading.View;

namespace Trading.Logic
{
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
            }
        }
    }
}
