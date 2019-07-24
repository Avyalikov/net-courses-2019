using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DoorsAndLevelsGame
{

    class Game
    {
        private int Level { get; set; }
        private int selectedNum;
        private int[] genNumbers;
        private bool Exit { get; set; }
        private Stack<int> selectedNumbersHistory;
        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutputComponent ioComp;
        private readonly IDoorsNumbersGenerator doorsGenerator;
        private readonly ISettingsProvider settings;
        private readonly GameSettings gameSettings;
       

        public Game(IPhraseProvider phraseProvider, IInputOutputComponent ioComponent, IDoorsNumbersGenerator doorsGenerator, ISettingsProvider settingsProvider)
        {
            this.phraseProvider = phraseProvider;
            this.ioComp = ioComponent;
            this.doorsGenerator = doorsGenerator;
            this.settings = settingsProvider;
            this.gameSettings = settings.GetGameSettings();
            this.selectedNumbersHistory = new Stack<int>();
            this.Level = 1;
            
        }

        private int GetUserNumber()
        {
            bool isNumber = false;
            int enteredNum;
            do
            {
                if (int.TryParse(Console.ReadLine(), out enteredNum))
                {
                    ioComp.WriteOutput(phraseProvider.GetPhrase("Selected") + $"{enteredNum}");
                    if (this.CheckInput(enteredNum) != -1)
                    {
                        isNumber = true;
                    }
                }

                else
                {
                    ioComp.WriteOutput(phraseProvider.GetPhrase("NotNumber"));
                }
            }

            while (!isNumber);
            return enteredNum;
        }



        private int CheckInput(int num)
        {
            int index = Array.IndexOf(this.genNumbers, num);
            if (index == -1)
            {
                ioComp.WriteOutput(phraseProvider.GetPhrase("WrongValue"));
            }
            return index;
        }


        //Calculate door numbers for next level
        private int[] CountValues(int[] numbers, int num)
        {

            if (num != 0)
            {
                try
                {
                    checked
                    {
                        for (int i = 0; i < numbers.Length; i++)
                        {
                            numbers[i] *= num;
                        }

                        return numbers;
                    }
                }
                catch (OverflowException)
                {
                    ioComp.WriteOutput(phraseProvider.GetPhrase("Maximum") + phraseProvider.GetPhrase("GameOver"));
                    this.Exit = true;
                    return numbers;
                }

            }


            else
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    numbers[i] /= this.selectedNumbersHistory.Peek();
                }
                selectedNumbersHistory.Pop();
                return numbers;
            }
        }

        private void PrintDoorsNumbers()
        {
            StringBuilder doorsToPrint = new StringBuilder();
            foreach (int value in this.genNumbers)
            {
                doorsToPrint.Append(value + " ");
            }

            ioComp.WriteOutput(phraseProvider.GetPhrase("Level") + $" {Level}\n" + phraseProvider.GetPhrase("Select") +
                $"{doorsToPrint}");
        }

        

        public void PlayGame()
        {
            phraseProvider.ReadResourceFile();
            genNumbers = this.doorsGenerator.GenerateNumbers(gameSettings.DoorsQuantity, gameSettings.MaxNumber);
            while (!this.Exit)
            {
                PrintDoorsNumbers();
                selectedNum = GetUserNumber();
                if (selectedNum != 0)
                {
                    selectedNumbersHistory.Push(selectedNum);
                    CountValues(genNumbers, selectedNum);
                    this.Level++;
                }

                else
                {
                    if (this.Level != 1)
                    {
                        CountValues(this.genNumbers, selectedNum);
                        this.Level--;
                    }
                    else
                    {
                        ioComp.WriteOutput(phraseProvider.GetPhrase("GameOver"));
                        this.Exit = true;
                    }
                }
            }
        }
    }
}

