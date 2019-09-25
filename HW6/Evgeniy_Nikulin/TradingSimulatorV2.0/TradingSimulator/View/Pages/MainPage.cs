namespace TradingSimulator.View.Pages
{
    using Core.Interfaces;
    using System.Collections.Generic;

    public class MainPage : IPage
    {
        private readonly IPhraseProvider provider;

        public string Header { get => provider.GetPhrase(Phrase.HeaderMain); }

        public string Description { get => provider.GetPhrase(Phrase.DescriptionMain); }

        public List<string> buttons = new List<string>();

        public List<string> Buttons { get => buttons; }

        public MainPage(IPhraseProvider provider)
        {
            this.provider = provider;

            buttons = new List<string>();

            buttons.Add($"1. {provider.GetPhrase(Phrase.HeaderHistory)}");
            buttons.Add($"2. {provider.GetPhrase(Phrase.HeaderTraderList)}");
            buttons.Add($"3. {provider.GetPhrase(Phrase.HeaderTraderManaging)}");
        }

        public string GetPage(string res)
        {
            switch (res)
            {
                case "1":
                    return provider.GetPhrase(Phrase.HeaderHistory);
                case "2":
                    return provider.GetPhrase(Phrase.HeaderTraderList);
                case "3":
                    return provider.GetPhrase(Phrase.HeaderTraderManaging);
                default:
                    return provider.GetPhrase(Phrase.HeaderMain);
            }
        }
    }
}