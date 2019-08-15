namespace TradingView
{
    using TradingView.Interface;

    internal class SettingsByProvider
    {
        private static SettingsByProvider setting = null;

        public readonly IPhraseProvider phraseProvider;
        public readonly IIOProvider iOProvider;

        protected SettingsByProvider(IPhraseProvider phraseProvider, IIOProvider iOProvider)
        {
            this.phraseProvider = phraseProvider;
            this.iOProvider = iOProvider;
        }

        public static SettingsByProvider Initialize(IPhraseProvider phraseProvider, IIOProvider iOProvider)
        {
            if (setting == null)
                setting = new SettingsByProvider(phraseProvider, iOProvider);
            return setting;
        }
    }
}
