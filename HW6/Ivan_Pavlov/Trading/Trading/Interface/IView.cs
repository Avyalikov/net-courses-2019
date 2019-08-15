namespace Trading.Interface
{
    public interface IView
    {
        int IndexMain();

        void PrintAllUsers(string userInfo);

        void PrintOrangeZone(string users);

        void PrintBlackZone(string users);

        void PrintAllStocks(string stocksInfo);

        string ChooseStock(bool Valid = false);

        string NewPrice(bool Valid = false);

        string EnterSurname(bool Valid = false);

        string EnterName(bool Valid = false);

        string EnterPhone(bool Valid = false);

        string EnterBalance(bool Valid = false);

        void UserCreated(string userInfo);
    }
}
