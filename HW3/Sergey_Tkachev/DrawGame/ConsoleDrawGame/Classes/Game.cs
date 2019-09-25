namespace ConsoleDrawGame.Classes
{
    using ConsoleDrawGame.Interfaces;

    internal class Game
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutput io;
        private readonly ISettingsProvider settingsProvider;
        private readonly IBoard board;
        private readonly GameSettings gameSettings;

        private int selectedNum;

        public Game(
            IPhraseProvider phraseProvider,
            IInputOutput io,
            ISettingsProvider settingsProvider,
            IBoard board)
        {
            this.phraseProvider = phraseProvider;
            this.io = io;
            this.settingsProvider = settingsProvider;
            this.board = board;
            this.gameSettings = this.settingsProvider.GetGameSettings();
            this.draw = this.PrintBoard;
        }

        private delegate void Draw(IBoard board);

        private Draw draw = null;

        public void Run()
        {
            while (true)
            {
                this.io.WriteOutput(this.phraseProvider.GetPhrase("WelcomeStart"));
                this.io.WriteOutput($"{this.gameSettings.ExitCode}");
                this.io.WriteOutput(this.phraseProvider.GetPhrase("WelcomeEnd"));
                this.io.WriteOutput(this.phraseProvider.GetPhrase("Instructions"));

                do
                {
                    this.io.WriteOutput(this.phraseProvider.GetPhrase("Select"));

                    this.selectedNum = this.GetInt();

                    if (this.selectedNum == this.gameSettings.ExitCode)
                    {
                        break;
                    }
                }
                while (!this.Contains(this.gameSettings.NumberOfChoices, this.selectedNum));

                if (this.selectedNum == this.gameSettings.ExitCode)
                {
                    this.io.WriteOutput(this.phraseProvider.GetPhrase("Thanks"));
                    break;
                }

                switch (this.selectedNum)
                {
                    case 1:
                        this.draw += this.PrintDot;
                        break;
                    case 2:
                        this.draw += this.PrintVertical;
                        break;
                    case 3:
                        this.draw += this.PrintHorizontal;
                        break;
                    case 4:
                        this.draw += this.PrintOtherCurve;
                        break;
                    default:
                        break;
                }

                this.io.ClearConsole();
                this.draw(this.board);
            }

            this.io.ReadKey();
        }

        private void PrintBoard(IBoard board)
        {
            board.PrintBoard();
        }

        private void PrintDot(IBoard board)
        {
            board.PrintDot();
        }

        private void PrintVertical(IBoard board)
        {
            board.PrintVertical();
        }

        private void PrintHorizontal(IBoard board)
        {
            board.PrintHorizontal();
        }

        private void PrintOtherCurve(IBoard board)
        {
            board.PrintOtherCurve();
        }

        /// <summary>Checks if entered number is integer, if not then number should be entered again.</summary>
        /// <returns></returns>
        private int GetInt()
        {
            while (true)
            {
                if (!int.TryParse(this.io.ReadInput(), out int enteredNum))
                {
                    this.io.WriteOutput(this.phraseProvider.GetPhrase("Incorrect"));
                }
                else
                {
                    return enteredNum;
                }
            }
        }

        /// <summary>Returns true if value more then zero and less or equal maxValue</summary>
        /// <param name="maxValue">Maximal value.</param>
        /// <param name="element">Element to compare.</param>
        /// <returns></returns>
        private bool Contains(int maxValue, int element)
        {
            if (element > 0 && element <= maxValue)
            {
                return true;
            }

            return false;
        }
    }
}
