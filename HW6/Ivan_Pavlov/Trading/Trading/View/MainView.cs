namespace Trading.View
{
    using System.Text;
    using Trading.Infrastructure;
    using Trading.Interface;

    public static class MainView
    {
        private static readonly IPhraseProvider phraseProvider;
        private static readonly IIOProvider iOProvider;

        static MainView()
        {
            phraseProvider = SettingsByProvider.phraseProvider;
            iOProvider = SettingsByProvider.iOProvider;
        }

        public static int IndexMain()
        {           
            return SelectFeatureFromFeatureList();
        }

        private static int SelectFeatureFromFeatureList(string IfInputError = "")
        {
            PrintFeatureList(IfInputError);

            if (int.TryParse(iOProvider.ReadLine(), out int UserSelect))
                return UserSelect;
            else
                return SelectFeatureFromFeatureList(phraseProvider.GetPhrase("InputError")); 
        }

        private static void PrintFeatureList(string IfInputError = "")
        {
            iOProvider.Clear();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(phraseProvider.GetPhrase("WelcomeMain"));
            sb.AppendLine(string.Format("1. {0}", phraseProvider.GetPhrase("StartTrading")));
            sb.AppendLine(string.Format("2. {0}", phraseProvider.GetPhrase("UsersList")));
            sb.AppendLine(string.Format("3. {0}", phraseProvider.GetPhrase("CreateUser")));
            sb.AppendLine(string.Format("4. {0}", phraseProvider.GetPhrase("StocksList")));
            sb.AppendLine(string.Format("5. {0}", phraseProvider.GetPhrase("ChangeStockPrice")));
            sb.AppendLine(string.Format("6. {0}", phraseProvider.GetPhrase("OrangeZone")));
            sb.AppendLine(string.Format("7. {0}", phraseProvider.GetPhrase("BlackZone")));
            sb.AppendLine(string.Format("8. {0}", phraseProvider.GetPhrase("CreateStock")));
            sb.AppendLine(IfInputError);
            iOProvider.WriteLine(sb.ToString());
        }
    }
}
