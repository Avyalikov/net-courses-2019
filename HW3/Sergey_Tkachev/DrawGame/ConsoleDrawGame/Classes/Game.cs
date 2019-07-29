using ConsoleDrawGame.Interfaces;

namespace ConsoleDrawGame.Classes
{
    class Game
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutput io;
        private readonly ISettingsProvider settingsProvider;
        private readonly IBoard board;
        private readonly GameSettings gameSettings;

        private delegate void Draw();
        private int selectedNum;
        Draw draw = null;

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
            draw = board.PrintBoard;
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
        /// <summary>Retrns true if value more then zero and less or equal maxValue</summary>
        /// <param name="maxValue">Maximal value.</param>
        /// <param name="element">Element to compare.</param>
        /// <returns></returns>
        public bool Contains(int maxValue, int element)
        {
            if (element > 0 && element <= maxValue)
                return true;
            return false;
        }

        public void Run()
        {
            while (true)
            {
                io.WriteOutput(phraseProvider.GetPhrase("WelcomeStart"));
                io.WriteOutput($"{gameSettings.ExitCode}");
                io.WriteOutput(phraseProvider.GetPhrase("WelcomeEnd"));
                io.WriteOutput(phraseProvider.GetPhrase("Instructions"));

                do
                {
                    io.WriteOutput(phraseProvider.GetPhrase("Select"));

                    InputCheck();

                    if (selectedNum == gameSettings.ExitCode)
                        break;

                } while (!Contains(gameSettings.NumberOfChoices, selectedNum));

                if (selectedNum == gameSettings.ExitCode)
                {
                    io.WriteOutput(phraseProvider.GetPhrase("Thanks"));
                    break;
                }

                switch (selectedNum)
                {
                    case 1:
                        draw += board.PrintDot;
                        break;
                    case 2:
                        draw += board.PrintVertical;
                        break;
                    case 3:
                        draw += board.PrintHorizontal;
                        break;
                    case 4:
                        draw += board.PrintOtherCurve;
                        break;
                    default:
                        break;
                }

                io.ClearConsole();
                draw();
            }

            io.ReadKey();
        }
    }
}
