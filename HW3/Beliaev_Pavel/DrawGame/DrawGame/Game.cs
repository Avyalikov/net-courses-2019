namespace DrawGame
{
using System;
using DrawGame.Interfaces;

    public enum InputCheckResult
    {
        Dot,
        VLine,
        HLine,
        Diamond,
        Clear,
        Invalid,
        Exit
    }

    public enum KeysForPhrases
    {
        Intro,
        ExitKey,
        SelectWhatToDraw,
        ClearKey,
        InvalidInput,
        Goodbye,
        CloseProg
    }

    internal class Game
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutput inputOutputModule;
        private readonly IDraw drawModule;
        private readonly IBoard board;

        private readonly GameSettings gameSettings;
        private DrawDelegate draw;

        public Game(IPhraseProvider phraseProvider, IInputOutput inputOutputModule, IDraw drawModule, ISettingsProvider settingsProvider, IBoard board)
        {
            this.phraseProvider = phraseProvider;
            this.inputOutputModule = inputOutputModule;
            this.drawModule = drawModule;
            this.board = board;

            try
            {
                this.gameSettings = settingsProvider.GetGameSettings();
            }
            catch (ArgumentException ex)
            {
                inputOutputModule.WriteOutput(ex.Message);
                this.gameSettings = null;
                return;
            }

            this.board.BoardSizeY = this.gameSettings.BoardHeight;
            this.board.BoardSizeX = this.gameSettings.BoardWidth;
        }

        private delegate void DrawDelegate(IBoard board);

        public void Run()
        {
            bool exit = false;
            string userInput;
            InputCheckResult checkResult;

            if (this.gameSettings == null)
            {
                return;
            }

            if (this.TestLangPack() == false)
            {
                return;
            }

            this.inputOutputModule.WriteOutput(this.phraseProvider.GetPhrase(this.gameSettings.LangPackName, KeysForPhrases.Intro));
            this.inputOutputModule.ReadInput();

            while (!exit)
            {
                this.drawModule.ClearConsole(this.board);
                this.drawModule.DrawBoard(this.board);

                if (this.draw != null)
                {
                    this.draw(this.board);
                }

                this.ShowInfo();
                userInput = this.inputOutputModule.ReadInput();

                if (string.IsNullOrEmpty(userInput))
                {
                    this.inputOutputModule.WriteOutput(this.phraseProvider.GetPhrase(this.gameSettings.LangPackName, KeysForPhrases.InvalidInput));
                    continue;
                }

                checkResult = this.CheckIfInputIsValid(userInput);

                switch (checkResult)
                {
                    case InputCheckResult.Invalid:
                        {
                            this.inputOutputModule.WriteOutput(this.phraseProvider.GetPhrase(this.gameSettings.LangPackName, KeysForPhrases.InvalidInput));
                            continue;
                        }

                    case InputCheckResult.Exit:
                        {
                            exit = true;
                            break;
                        }

                    case InputCheckResult.Diamond:
                        {
                            this.draw += this.drawModule.DrawDiamond;  
                            break;
                        }

                    case InputCheckResult.Dot:
                        {
                            this.draw += this.drawModule.DrawSimpleDot;
                            break;
                        }

                    case InputCheckResult.HLine:
                        {
                            this.draw += this.drawModule.DrawHorizontalLine;
                            break;
                        }

                    case InputCheckResult.VLine:
                        {
                            this.draw += this.drawModule.DrawVerticalLine;
                            break;
                        }

                    case InputCheckResult.Clear:
                        {
                            this.draw = null;
                            break;
                        }
                }
            }

            this.inputOutputModule.WriteOutput(this.phraseProvider.GetPhrase(this.gameSettings.LangPackName, KeysForPhrases.Goodbye));
            this.inputOutputModule.WriteOutput(this.phraseProvider.GetPhrase(this.gameSettings.LangPackName, KeysForPhrases.CloseProg));

            this.inputOutputModule.ReadKey();
        }

        private InputCheckResult CheckIfInputIsValid(string input)
        {
            if (input == this.gameSettings.ExitButton.ToString())
            {
                return InputCheckResult.Exit;
            }

            if (input == this.gameSettings.ClearButton.ToString())
            {
                return InputCheckResult.Clear;
            }

            try
            {
                int numInput = Convert.ToInt32(input);
                int i = 1;
                foreach (InputCheckResult figure in Enum.GetValues(typeof(InputCheckResult)))
                {
                    if (i == numInput)
                    {
                        return figure;
                    }

                    i++;
                }

                return InputCheckResult.Invalid;
            }
            catch (System.FormatException)
            {
                return InputCheckResult.Invalid;
            }
        }

        private bool TestLangPack()
        {
            try
            {
                foreach (KeysForPhrases phraseKey in Enum.GetValues(typeof(KeysForPhrases)))
                {
                    this.phraseProvider.GetPhrase(this.gameSettings.LangPackName, phraseKey);
                }
            }
            catch (System.ArgumentException ex)
            {
                this.inputOutputModule.WriteOutput(ex.Message);
                return false;
            }

            return true;
        }

        private void ShowInfo()
        {
            this.inputOutputModule.WriteOutput(this.phraseProvider.GetPhrase(this.gameSettings.LangPackName, KeysForPhrases.SelectWhatToDraw));
            this.inputOutputModule.WriteOutput(this.gameSettings.ClearButton.ToString());
            this.inputOutputModule.WriteOutput(this.phraseProvider.GetPhrase(this.gameSettings.LangPackName, KeysForPhrases.ClearKey));
            this.inputOutputModule.WriteOutput(this.gameSettings.ExitButton.ToString());
            this.inputOutputModule.WriteOutput(this.phraseProvider.GetPhrase(this.gameSettings.LangPackName, KeysForPhrases.ExitKey));
        }
    }
}
