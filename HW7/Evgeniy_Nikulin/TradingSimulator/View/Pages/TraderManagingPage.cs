namespace TradingSimulator.View.Pages
{
    using System.Collections.Generic;
    using Core.Interfaces;

    public class TraderManagingPage : IPage
    {
        private readonly IPhraseProvider provider;

        public string Header { get => provider.GetPhrase(Phrase.HeaderTraderManaging); }

        public string Description { get => provider.GetPhrase(Phrase.DescriptionTraderManaging); }

        public List<string> buttons;
        public List<string> Buttons { get => buttons; }
        public TraderManagingPage(IPhraseProvider provider)
        {
            this.provider = provider;

            buttons = new List<string>();

            buttons.Add($"1. {provider.GetPhrase(Phrase.HeaderAddTrader)}");
            buttons.Add($"2. {provider.GetPhrase(Phrase.HeaderAddShare)}");
            buttons.Add($"3. {provider.GetPhrase(Phrase.HeaderChandeShare)}");
            buttons.Add($"4. {provider.GetPhrase(Phrase.ButtonMenu)}");
        }
        public string GetPage(string res)
        {
            switch (res)
            {
                case "1":
                    return provider.GetPhrase(Phrase.HeaderAddTrader);
                case "2":
                    return provider.GetPhrase(Phrase.HeaderAddShare);
                case "3":
                    return provider.GetPhrase(Phrase.HeaderChandeShare);
                default:
                    return provider.GetPhrase(Phrase.HeaderMain);
            }
        }
    }
}