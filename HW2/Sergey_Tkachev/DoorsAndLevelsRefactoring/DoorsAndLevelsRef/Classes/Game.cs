using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevelsRef
{
    class Game
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutput io;
        private readonly ISettingsProvider settingsProvider;
        private readonly IArrayGenerator arrayGenerator;
        private readonly IOperationWithData operationWithData;

        private readonly GameSettings gameSettings;

        private int[] levelNumbers;
        private Stack<int> history;
        private int selectedNum;
        private int currentLevel;

        public Game(IPhraseProvider phraseProvider,
                    IInputOutput io,
                    ISettingsProvider settingsProvider,
                    IArrayGenerator arrayGenerator,
                    IOperationWithData operationWithData)
        {
            this.phraseProvider = phraseProvider;
            this.io = io;
            this.settingsProvider = settingsProvider;
            this.arrayGenerator = arrayGenerator;
            this.operationWithData = operationWithData;

            this.gameSettings = this.settingsProvider.GetGameSettings();
            this.history = new Stack<int>();
            currentLevel = 1;
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

            levelNumbers = arrayGenerator.GenerateArray(gameSettings.DoorsAmount);

            io.WriteOutput(phraseProvider.GetPhrase("WelcomeStart"));
            io.WriteOutput($"{gameSettings.ExitCode}");
            io.WriteOutput(phraseProvider.GetPhrase("WelcomeEnd"));

            while (true)
            {
                io.WriteOutput(phraseProvider.GetPhrase("Level"));
                io.WriteOutput($"{currentLevel}");
                io.WriteOutput(phraseProvider.GetPhrase("TheDoorsAre"));
                io.printArray(levelNumbers);

                do
                {
                    io.WriteOutput(phraseProvider.GetPhrase("Select"));

                    InputCheck();

                    if (selectedNum == gameSettings.ExitCode)
                        break;

                } while (!operationWithData.Contains(levelNumbers, selectedNum));

                if (selectedNum == gameSettings.ExitCode)
                {
                    io.WriteOutput(phraseProvider.GetPhrase("Thanks"));
                    break;
                }
                else if (selectedNum == gameSettings.ExitDoorNumber)
                {
                    if (currentLevel > 1)
                    {
                        operationWithData.Divide(levelNumbers, history.Pop());
                        io.WriteOutput(phraseProvider.GetPhrase("YouSelected"));
                        io.WriteOutput($"{selectedNum}");
                        io.WriteOutput(phraseProvider.GetPhrase("Previous"));
                        currentLevel--;
                    }
                    else if (currentLevel == 1) {
                        io.WriteOutput(phraseProvider.GetPhrase("AlreadyFirst"));
                    }
                }
                else 
                {
                    if (currentLevel < gameSettings.MaxLevel)
                    {
                        operationWithData.Multiply(levelNumbers, selectedNum);
                        history.Push(selectedNum);
                        io.WriteOutput(phraseProvider.GetPhrase("YouSelected"));
                        io.WriteOutput($"{selectedNum}");
                        io.WriteOutput(phraseProvider.GetPhrase("Next"));
                        currentLevel++;
                    }
                    else {
                        io.WriteOutput(phraseProvider.GetPhrase("MaxLevelReached"));
                    }
                }

            }
            io.ReadKey();
        }
    }
}
