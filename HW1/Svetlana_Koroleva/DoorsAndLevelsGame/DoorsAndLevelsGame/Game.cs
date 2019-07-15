using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevelsGame
{
    
    class Game
    {
        
      public int Level { get; set; }
      public  Dictionary<int, int[]> levelsResults;



        public int[] GenerateNumbers()
        {
            int[] generatedNumbers = new int [5];
            Random random = new Random();
           
            for (int i=0; i<generatedNumbers.Length; i++)
            {
                generatedNumbers[i] = random.Next(0,9);
            }

            return generatedNumbers;
        }

        public int EnteringNumber()
        {
            try
            {
                Console.WriteLine("Enter the number from 0 till 9: ");
                int enteredNum = int.Parse(Console.ReadLine());
                if (enteredNum < 10 && enteredNum >= 0)
                    return enteredNum;
                else return EnteringNumber();
            }
            catch (Exception e)
            {   Console.WriteLine(e);           
                throw new Exception( );
            }
        }

        public int[] CountValues(int[] numbers, int num)
        {
            int[] multiplNumbers = new int[numbers.Length];
            for(int i=0; i<numbers.Length;i++)
            {
                multiplNumbers[i]=numbers[i]*num;
            }

            return multiplNumbers;
        }

        public void ShowCurrentResult()
        {
            
            Console.WriteLine($"Current Level {this.Level}");
            int[] results = this.levelsResults.Last().Value.ToArray();
            foreach(int value in results)
            { 
                Console.WriteLine($" {value} ");
            }

            //TO DO overwrite on dictionary methods
        }
        
        
      public void PlayGame()
        {
           
            int selectNum = this.EnteringNumber();
            if (this.Level == 0)
            {
                int[] genNumbers = this.GenerateNumbers();
                int[] levelResult = this.CountValues(genNumbers, selectNum);                
                this.Level++;
                levelsResults.Add(this.Level, levelResult);
                
               
            }
            else
            {
                if (selectNum != 0)
                {
                    int[] levelResult = this.CountValues(this.levelsResults[this.Level], selectNum);                   
                    this.Level++;
                    levelsResults.Add(this.Level, levelResult);
                    
                    
                }

                else {
                    this.Level++;
                    levelsResults.Add(this.Level, this.levelsResults[this.Level - 2] );//TO DO Create Method for changing levels (next/previous)
                    
                    return;                    
                }
            }

        }
           

    }
}
