using System;
using System.Collections.Generic;
using System.Linq;


namespace NumbersGame
{
    class Program
    {
        struct NumbersOperator  //Stucture for storing our numbers and working with them
        {
            private int x0, x1, x2, x3, x4;
            
            public void RandomNumbersGenerator()
            {
                Random rnd = new Random();
                x0 = 0;
                x1 = rnd.Next(1, 9);
                x2 = rnd.Next(1, 9);
                x3 = rnd.Next(1, 9);
                x4 = rnd.Next(1, 9);
            }
            public bool CheckNumber(int i)
            {     
                if (i == x1 || i == x2 || i == x3 || i == x4)
                {
                    return true;
                } else return false; 
            }
            public bool CheckWin()
            {
                if (x1 >= 200 || x2 >= 200 || x3 >= 200 || x4 >= 200)
                {
                    return false;
                }
                else return true;
            }
            public void LevelDown(NumbersOperator prevNum)
            {
                (x1, x2, x3, x4) = prevNum;
            }
            public void Deconstruct(out int x1, out int x2, out int x3, out int x4)
            {
                x1 = this.x1;
                x2 = this.x2;
                x3 = this.x3;
                x4 = this.x4;
            }
            public void LevelUp(int a)
            {
                x1 *= a;
                x2 *= a;
                x3 *= a;
                x4 *= a;
            } 
            public void WriteNumbers()
            {
                Console.WriteLine($"{x1} {x2} {x3} {x4} {x0}\n");
            }
        }
        // method for working with stack and input
        static void LevelContoller(Stack<NumbersOperator> levelsHolder, ref NumbersOperator num)
        {
            int i;
            for (bool b = false; b == false;)
            {
                Console.WriteLine("\n--------------------------");
                Console.Write("Enter one of this numbers: ");
                num.WriteNumbers();
                string input = Console.ReadLine();
                try
                {
                    if (input == "")
                    {
                        Console.WriteLine("\n!Invalid input!\n");
                        continue;
                    }
                    else i = Convert.ToInt32(input);
                }

                catch (System.FormatException) {
                    Console.WriteLine("\n!Invalid input!\n");
                    continue;
                }

                if (i == 0)
                {
                    if (!levelsHolder.Any()) { Console.WriteLine("\nThis is the 1st level!\n"); continue; }                 
                    num.LevelDown(levelsHolder.Pop());
                    b = true;
                }
                else if (num.CheckNumber(i))
                {
                    levelsHolder.Push(num);
                    num.LevelUp(i);
                    b = true;
                }
                else { Console.WriteLine("\n!Invalid input!\n"); }
            }
        }

        static void Main(string[] args)
        {
            Stack<NumbersOperator> levelsHolder = new Stack<NumbersOperator>();
            NumbersOperator num = new NumbersOperator();
            num.RandomNumbersGenerator();
            
            Console.WriteLine(@"Welcome to the game ""Doors and Levels"" 
Rules are simple enter one of the shown numbers and go to the next level.
When atleast one of the numbers will become bigger than 200 you WIN!");

            do {
                LevelContoller(levelsHolder, ref num);
            } while (num.CheckWin());


            Console.WriteLine("\n*****You WIN! Congratulations!*****\n");
            Console.ReadLine();
        }
    }
}
