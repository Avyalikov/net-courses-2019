using ConsoleDrawGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawGame.Classes
{
    class Game
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutput io;
        private readonly ISettingsProvider settingsProvider;
        private readonly IBoard board;

        private readonly GameSettings gameSettings;

        private int selectedNum;

        public Game(IPhraseProvider phraseProvider,
                    IInputOutput io,
                    ISettingsProvider settingsProvider,
                    IBoard board
                    )
        {
            this.phraseProvider = phraseProvider;
            this.io = io;
            this.settingsProvider = settingsProvider;
            this.board = board;

            this.gameSettings = this.settingsProvider.GetGameSettings();
        }

        /// <summary>Checks if entered number is integer, if not then number should be entered again.</summary>
        /// <returns></returns>
        private int InputCheck()
        {
            while (true)
                if (!int.TryParse(io.ReadInput(), out int enteredNum))
                    io.WriteOutput(phraseProvider.GetPhrase("Incorrect"));
                else
                    return selectedNum = enteredNum;
        }

        public void Run()
        {
            board.PrintBoard();
            io.ReadKey();
        }
    }
}
