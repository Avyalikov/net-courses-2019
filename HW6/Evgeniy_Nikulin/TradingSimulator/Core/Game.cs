namespace TradingSimulator.Core
{
    using Components;
    using Interfaces;
    using System;
    using TradingSimulator.Interfaces;

    public class Game : IGame
    {
        private readonly IInputOutput io;
        private readonly IPhraseProvider phraseProvider;
        private readonly GameSettings gs;

        private readonly ITraderMenager tradeMenager;

        private readonly Random rd = new Random();

        //private readonly ITrade /make trade/
        //private readonly IState /some state for menu/
        //private readonly IMenu?? /menu controller/

        //event Draw /draw menu and trade field/

        public Game(
            IInputOutput io,
            IPhraseProvider phraseProvider,
            GameSettings gs)
        {
            this.io = io;
            this.phraseProvider = phraseProvider;
            this.gs = gs;

            tradeMenager = new TraderMenager(io, 50, 0);
        }

        public void Run()
        {
            io.Print(phraseProvider.GetPhrase(Phrase.Welcome) + Environment.NewLine);

            tradeMenager.Run();

            string input = io.Input();

        }
    }
}