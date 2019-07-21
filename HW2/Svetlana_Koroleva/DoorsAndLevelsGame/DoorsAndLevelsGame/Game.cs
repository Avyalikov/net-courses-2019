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
        private bool firstRun = true;
        public bool Exit { get; private set; }
        private Stack<int> selectedNumbersHistory;

        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutputComponent ioComp;
        private readonly IDoorsNumbersGenerator doorsGenerator;
        private readonly ISettingsProvider settings;
        private readonly INumberSelector numberSelector;

        private readonly GameSettings gameSettings;

        public Game(IPhraseProvider phraseProvider, IInputOutputComponent ioComponent, IDoorsNumbersGenerator doorsGenerator, ISettingsProvider settingsProvider,
            INumberSelector numberSelector)
        {
            this.phraseProvider= phraseProvider;
            this.ioComp = ioComponent;
            this.doorsGenerator = doorsGenerator;
            this.settings = settingsProvider;
            this.gameSettings = settings.GetGameSettings();
            this.numberSelector = numberSelector;

            this.selectedNumbersHistory = new Stack<int>();            
            this.Level = 1;
        }



        //Input check up
        private bool CheckInput(int num)
        {

            bool check = false;
            foreach (int val in this.genNumbers)
            {
                if (num == val)
                {
                    check = true;
                    break;
                }

            }

            if (check == false)
            {
                ioComp.WriteOutput(phraseProvider.GetPhrase("WrongNumber"));

            }

            return check;


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
                catch (OverflowException e)
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

            ioComp.WriteOutput(phraseProvider.GetPhrase("Level")+ $" {Level}"  + phraseProvider.GetPhrase("Select")+
                $"{doorsToPrint}");
        }



        public void PlayGame()
        {
            if (this.Level == 1 && firstRun)
            {
                firstRun = false;                
                genNumbers = this.doorsGenerator.generatedNumbers(gameSettings.doorsQuantity);
                PrintDoorsNumbers();
                selectedNum = this.numberSelector.EnteringNumber();                
                bool check = CheckInput(selectedNum);
                if (check == true)
                {
                    if (selectedNum != 0)
                    {
                        selectedNumbersHistory.Push(selectedNum);                        
                        CountValues(genNumbers, selectedNum);
                        this.Level++;

                    }


                    else
                    {
                        ioComp.WriteOutput(phraseProvider.GetPhrase("GameOver"));
                        this.Exit = true;
                    }
                }  
                

            }
            else
            {
                PrintDoorsNumbers();

                selectedNum = this.numberSelector.EnteringNumber();
                bool check = CheckInput(selectedNum);
                if (check == true)
                {
                    if (selectedNum != 0)
                    {
                        selectedNumbersHistory.Push(selectedNum);
                        CountValues(this.genNumbers, selectedNum);
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
}
