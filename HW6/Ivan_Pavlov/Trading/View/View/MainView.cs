namespace TradingView
    {
    using System.Text;
    using TradingView.Interface;

    internal static class MainView
    {
        private static readonly IPhraseProvider phraseProvider;
        private static readonly IIOProvider iOProvider;

        static MainView()
        {
            phraseProvider = ViewProvider.settings.phraseProvider;
            iOProvider = ViewProvider.settings.iOProvider;
        }

        public static int IndexMain(bool TradeStart)
        {           
            return SelectFeatureFromFeatureList(TradeStart);
        }

        private static int SelectFeatureFromFeatureList(bool TradeStart, string IfInputError = "")
        {
            PrintFeatureList(TradeStart, IfInputError);

            if (int.TryParse(iOProvider.ReadLine(), out int UserSelect))
                return UserSelect;
            else
                return SelectFeatureFromFeatureList(TradeStart, phraseProvider.GetPhrase("InputError")); 
        }

        private static void PrintFeatureList(bool TradeStart, string IfInputError = "")
        {
            iOProvider.Clear();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(phraseProvider.GetPhrase("WelcomeMain"));
            if (!TradeStart)
                sb.AppendLine(string.Format("1. {0}", phraseProvider.GetPhrase("StartTrading")));
            else
                sb.AppendLine(string.Format("1. {0}", phraseProvider.GetPhrase("StopTrading")));
            sb.AppendLine(string.Format("2. {0}", phraseProvider.GetPhrase("UsersList")));
            sb.AppendLine(string.Format("3. {0}", phraseProvider.GetPhrase("CreateUser")));
            sb.AppendLine(string.Format("4. {0}", phraseProvider.GetPhrase("StocksList")));
            sb.AppendLine(string.Format("5. {0}", phraseProvider.GetPhrase("ChangeStockPrice")));
            sb.AppendLine(string.Format("6. {0}", phraseProvider.GetPhrase("OrangeZone")));
            sb.AppendLine(string.Format("7. {0}", phraseProvider.GetPhrase("BlackZone")));
            
            sb.AppendLine(IfInputError);
            iOProvider.WriteLine(sb.ToString());
        }
    }
}
