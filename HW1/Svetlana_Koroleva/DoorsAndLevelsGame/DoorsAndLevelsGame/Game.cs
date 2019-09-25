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

        public Game()
        {
            this.levelsSelection = new Dictionary<int, int>();
            this.Level = 1;
        }


        //Generate doors
        private int[] GenerateNumbers()
        {
            int[] generatedNumbers = new int[5];
            Random random = new Random();

            for (int i = 0; i < generatedNumbers.Length - 1; i++)
            {
                generatedNumbers[i] = random.Next(1, 9);
            }

            generatedNumbers[4] = 0;

            return generatedNumbers;
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

                    Console.WriteLine($"You selected  {enteredNum}");
                    isNumber = true;

                }
                
                else
                {
                    Console.WriteLine("You entered not a number, try again");
                    

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
                Console.WriteLine("You entered wrong number");

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

                    Console.WriteLine("Вы достигли максимума");
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

            Console.WriteLine($@"Level {Level}  Select the door:
{doorsToPrint}");
        }



        public void PlayGame()
        {
            if (this.Level == 1 && firstRun)
            {
                firstRun = false;
                genNumbers = this.GenerateNumbers();
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
                        Console.WriteLine("Game over!");
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
                            Console.WriteLine("Game over!");
                            this.Exit = true;
                        }

                    }
                }
            }
        }
    }
}
