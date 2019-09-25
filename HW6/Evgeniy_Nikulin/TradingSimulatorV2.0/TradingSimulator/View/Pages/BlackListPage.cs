namespace TradingSimulator.View.Pages
{
    using System.Collections.Generic;
    using Core.Interfaces;

    public class BlackListPage : IPage
    {
        private readonly IPhraseProvider provider;

        public string Header { get => provider.GetPhrase(Phrase.HeaderBlackList); }

        public string Description { get => provider.GetPhrase(Phrase.DescriptionBlackList); }

        public List<string> buttons;
        public List<string> Buttons { get => buttons; }
        public BlackListPage(IPhraseProvider provider)
        {
            this.provider = provider;

            buttons = new List<string>();

            buttons.Add($"1. {provider.GetPhrase(Phrase.ButtonRefresh)}");
            buttons.Add($"2. {provider.GetPhrase(Phrase.ButtonBack)}");
            buttons.Add($"3. {provider.GetPhrase(Phrase.ButtonMenu)}");
        }
        public string GetPage(string res)
        {
            switch (res)
            {
                case "1":
                    return provider.GetPhrase(Phrase.HeaderBlackList);
                case "2":
                    return provider.GetPhrase(Phrase.HeaderTraderList);
                case "3":
                default:
                    return provider.GetPhrase(Phrase.HeaderMain);
            }
        }
    }
}