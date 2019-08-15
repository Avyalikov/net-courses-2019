namespace Trading.Infrastructure
{
    using Trading.Interface;
    using Trading.View;

    public class ViewProvider : IView
    {
        public string ChooseStock(bool Valid = false)
        {
            return ChangeStock.ChooseStock(Valid);
        }

        public string EnterBalance(bool Valid = false)
        {
            return CreateUser.EnterBalance(Valid);
        }

        public string EnterName(bool Valid = false)
        {
            return CreateUser.EnterName(Valid);
        }

        public string EnterPhone(bool Valid = false)
        {
            return CreateUser.EnterPhone(Valid);
        }

        public string EnterSurname(bool Valid = false)
        {
            return CreateUser.EnterSurname(Valid);
        }

        public int IndexMain()
        {
            return MainView.IndexMain();
        }

        public string NewPrice(bool Valid = false)
        {
            return ChangeStock.NewPrice(Valid);
        }

        public void PrintAllStocks(string stocksInfo)
        {
            PrintInfo.PrintAllStocks(stocksInfo);
        }

        public void PrintAllUsers(string userInfo)
        {
            PrintInfo.PrintAllUsers(userInfo);
        }

        public void PrintBlackZone(string users)
        {
            PrintInfo.PrintBlackZone(users);
        }

        public void PrintOrangeZone(string users)
        {
            PrintInfo.PrintOrangeZone(users);
        }

        public void UserCreated(string userInfo)
        {
            CreateUser.UserCreated(userInfo);
        }
    }
}
