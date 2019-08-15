namespace TradingView
{
    using TradingView.Interface;

    internal class PrintInfo
    {
        private static readonly IPhraseProvider phraseProvider;
        private static readonly IIOProvider iOProvider;

        static PrintInfo()
        {
            phraseProvider = ViewProvider.settings.phraseProvider;
            iOProvider = ViewProvider.settings.iOProvider;
        }

        public static void PrintAllUsers(string usersInfo)
        {
            iOProvider.Clear();
            iOProvider.WriteLine(phraseProvider.GetPhrase("UsersInfo"));           
            iOProvider.WriteLine(usersInfo);
            iOProvider.WriteLine(phraseProvider.GetPhrase("BackToMain"));
            iOProvider.ReadKey();
        }

        public static void PrintOrangeZone(string users)
        {
            iOProvider.Clear();
            if (string.IsNullOrEmpty(users))
                iOProvider.WriteLine(phraseProvider.GetPhrase("OrangeZoneNull"));
            else
            {
                iOProvider.WriteLine(phraseProvider.GetPhrase("OrangeZoneList"));
                iOProvider.WriteLine(users);
            }
            iOProvider.WriteLine(phraseProvider.GetPhrase("BackToMain"));
            iOProvider.ReadKey();
        }

        public static void PrintBlackZone(string users)
        {
            iOProvider.Clear();
            if (string.IsNullOrEmpty(users))
                iOProvider.WriteLine(phraseProvider.GetPhrase("BlackZoneNull"));
            else
            {
                iOProvider.WriteLine(phraseProvider.GetPhrase("BlackZoneList"));
                iOProvider.WriteLine(users);
            }
            iOProvider.WriteLine(phraseProvider.GetPhrase("BackToMain"));
            iOProvider.ReadKey();
        }

        public static void PrintAllStocks(string stocksInfo)
        {
            iOProvider.Clear();
            iOProvider.WriteLine(phraseProvider.GetPhrase("StocksInfo"));
            iOProvider.WriteLine(stocksInfo);
            iOProvider.WriteLine(phraseProvider.GetPhrase("BackToMain"));
            iOProvider.ReadKey();
        }
    }
}
