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

        private delegate void Draw(IBoard board);
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
            draw = PrintBoard;
        }

        void PrintBoard(IBoard board)
        {
            board.PrintBoard();
        }

        void PrintDot(IBoard board)
        {
            board.PrintDot();
        }

        void PrintVertical(IBoard board)
        {
            board.PrintVertical();
        }

        void PrintHorizontal(IBoard board)
        {
            board.PrintHorizontal();
        }

        void PrintOtherCurve(IBoard board)
        {
            board.PrintOtherCurve();
        }

        /// <summary>Checks if entered number is integer, if not then number should be entered again.</summary>
        /// <returns></returns>
        private int GetInt()
        {
            while (true)
                if (!int.TryParse(io.ReadInput(), out int enteredNum))
                    io.WriteOutput(phraseProvider.GetPhrase("Incorrect"));
                else
                    return enteredNum;
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

                    selectedNum = GetInt();

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
                        draw += PrintDot;
                        break;
                    case 2:
                        draw += PrintVertical;
                        break;
                    case 3:
                        draw += PrintHorizontal;
                        break;
                    case 4:
                        draw += PrintOtherCurve;
                        break;
                    default:
                        break;
                }

                io.ClearConsole();
                draw(board);
            }

            io.ReadKey();
        }
    }
}
