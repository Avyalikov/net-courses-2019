namespace TradingSimulator.View.Pages
{
    using System.Collections.Generic;
    using Core.Interfaces;
    public class TraderListPage : IPage
    {
        private readonly IPhraseProvider provider;

        public string Header { get => provider.GetPhrase(Phrase.HeaderTraderList); }

        public string Description { get => provider.GetPhrase(Phrase.DescriptionTraderList); }

        public List<string> buttons;
        public List<string> Buttons { get => buttons; }
        public TraderListPage(IPhraseProvider provider)
        {
            this.provider = provider;

            buttons = new List<string>();

            buttons.Add($"1. {provider.GetPhrase(Phrase.HeaderWhiteList)}");
            buttons.Add($"2. {provider.GetPhrase(Phrase.HeaderOrangeList)}");
            buttons.Add($"3. {provider.GetPhrase(Phrase.HeaderBlackList)}");
            buttons.Add($"4. {provider.GetPhrase(Phrase.ButtonMenu)}");
        }
        public string GetPage(string res)
        {
            switch (res)
            {
                case "1":
                    return provider.GetPhrase(Phrase.HeaderWhiteList);
                case "2":
                    return provider.GetPhrase(Phrase.HeaderOrangeList);
                case "3":
                    return provider.GetPhrase(Phrase.HeaderBlackList);
                default:
                    return provider.GetPhrase(Phrase.HeaderMain);
            }
        }
    }
}