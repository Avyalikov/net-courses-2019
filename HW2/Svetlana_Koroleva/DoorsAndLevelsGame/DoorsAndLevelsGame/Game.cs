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
        private Dictionary<int, int> levelsSelection;

        private readonly IPhraseProvider phraseProvider;
        private readonly IInputOutputComponent ioComp;
        private readonly IDoorsNumbersGenerator doorsGenerator;

        public Game(IPhraseProvider phraseProvider, IInputOutputComponent ioComponent, IDoorsNumbersGenerator doorsGenerator)
        {
            this.phraseProvider= phraseProvider;
            this.ioComp = ioComponent;
            this.doorsGenerator = doorsGenerator;

            this.levelsSelection = new Dictionary<int, int>();
            this.Level = 1;
        }


        //User input
        private int EnteringNumber()
        {
            bool isNumber = false;
            int enteredNum;
            do
            {
                if (int.TryParse(Console.ReadLine(), out enteredNum))
                {

                    ioComp.WriteOutput(phraseProvider.GetPhrase("Selected")+$"{enteredNum}");
                    isNumber = true;

                }
                
                else
                {
                    ioComp.WriteOutput(phraseProvider.GetPhrase("NotNumber"));
                    

                }
            }

            while (!isNumber);
            return enteredNum;
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
                    numbers[i] /= levelsSelection.Values.Last();
                   
                }

                levelsSelection.Remove(levelsSelection.Keys.Last());

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
                genNumbers = this.doorsGenerator.generatedNumbers(5);
                PrintDoorsNumbers();
                selectedNum = this.EnteringNumber();
                bool check = CheckInput(selectedNum);
                if (check == true)
                {
                    if (selectedNum != 0)
                    {
                        levelsSelection.Add(this.Level, selectedNum);
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

                selectedNum = this.EnteringNumber();
                bool check = CheckInput(selectedNum);
                if (check == true)
                {
                    if (selectedNum != 0)
                    {
                        levelsSelection.Add(this.Level, selectedNum);
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
