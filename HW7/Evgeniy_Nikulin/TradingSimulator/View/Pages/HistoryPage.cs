namespace TradingSimulator.View.Pages
{
    using System.Collections.Generic;
    using Core.Interfaces;

    public class HistoryPage : IPage
    {
        private readonly IPhraseProvider provider;

        public string Header { get => provider.GetPhrase(Phrase.HeaderHistory); }

        public string Description { get => provider.GetPhrase(Phrase.DescriptionHistory); }

        public List<string> buttons;
        public List<string> Buttons { get => buttons; }
        public HistoryPage(IPhraseProvider provider)
        {
            this.provider = provider;

            buttons = new List<string>();

            buttons.Add($"1. {provider.GetPhrase(Phrase.ButtonRefresh)}");
            buttons.Add($"2. {provider.GetPhrase(Phrase.ButtonMenu)}");
        }
        public string GetPage(string res)
        {
            switch (res)
            {
                case "1":
                    return provider.GetPhrase(Phrase.HeaderHistory);
                case "2":
                default:
                    return provider.GetPhrase(Phrase.HeaderMain);
            }
        }
    }
}