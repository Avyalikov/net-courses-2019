namespace Trading.View
{
    using Trading.Infrastructure;
    using Trading.Interface;

    static class ChangeStock
    {
        private static readonly IPhraseProvider phraseProvider;
        private static readonly IIOProvider iOProvider;

        static ChangeStock()
        {
            phraseProvider = SettingsByProvider.phraseProvider;
            iOProvider = SettingsByProvider.iOProvider;
        }

        public static string ChooseStock(bool Valid = false)
        {
            iOProvider.WriteLine(phraseProvider.GetPhrase("EnterStockId"));
            if (Valid)
                iOProvider.WriteLine(phraseProvider.GetPhrase("InputError"));
            return iOProvider.ReadLine();
        }

        public static string NewPrice(bool Valid = false)
        {
            iOProvider.WriteLine(phraseProvider.GetPhrase("EnterNewPrice"));
            if (Valid)
                iOProvider.WriteLine(phraseProvider.GetPhrase("InputError"));
            return iOProvider.ReadLine();
        }
    }
}
