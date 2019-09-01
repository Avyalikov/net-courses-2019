namespace TradingSimulator.View
{
    using System;
    using System.Collections.Generic;
    using Core.Dto;
    using Core.Interfaces;
    using Pages;

    public class Menu
    {
        private readonly Point start;
        private readonly int whidth;
        private readonly int height;

        private readonly IInputOutput io;

        private IPage page;
        private readonly Dictionary<string, IPage> Pages;

        public Menu(
            Point start,
            int whidth,
            int height,
            IInputOutput io,
            IPhraseProvider phraseProvider)
        {
            this.start = start;
            this.whidth = whidth;
            this.height = height;

            this.io = io;

            Pages = new Dictionary<string, IPage>
            {
                { phraseProvider.GetPhrase(Phrase.HeaderMain), new MainPage(phraseProvider) },

                { phraseProvider.GetPhrase(Phrase.HeaderHistory), new HistoryPage(phraseProvider) },
                { phraseProvider.GetPhrase(Phrase.HeaderTraderList), new TraderListPage(phraseProvider) },
                { phraseProvider.GetPhrase(Phrase.HeaderTraderManaging), new TraderManagingPage(phraseProvider) },

                { phraseProvider.GetPhrase(Phrase.HeaderAddTrader), new AddTraderPage(phraseProvider) },
                { phraseProvider.GetPhrase(Phrase.HeaderAddShare), new AddSharePage(phraseProvider) },
                { phraseProvider.GetPhrase(Phrase.HeaderChandeShare), new ChangeSharePage(phraseProvider) },

                { phraseProvider.GetPhrase(Phrase.HeaderGreenList), new GreenListPage(phraseProvider) },
                { phraseProvider.GetPhrase(Phrase.HeaderOrangeList), new OrangeListPage(phraseProvider) },
                { phraseProvider.GetPhrase(Phrase.HeaderBlackList), new BlackListPage(phraseProvider) }
            };
            page = Pages[phraseProvider.GetPhrase(Phrase.HeaderMain)];
        }

        public void PrintHeader()
        {
            io.Clear(start, (start.x + whidth, start.y + height));

            io.CursorPosition = start;
            io.Print(page.Header + Environment.NewLine);
        }

        public void PrintDiscription()
        {
            io.Print(Environment.NewLine);
            io.Print(page.Description + Environment.NewLine);
        }

        public void Action(Dictionary<string, Action> Actions)
        {
            Actions[page.Header]();
        }

        public void PrintButtons()
        {
            io.Print(Environment.NewLine);

            foreach (var b in page.Buttons)
            {
                io.Print(b + Environment.NewLine);
            }            
        }

        public void SwitchPage()
        {
            io.Print(Environment.NewLine + "> ");

            string input = io.Input();

            page = Pages[page.GetPage(input)];            
        }
    }
}