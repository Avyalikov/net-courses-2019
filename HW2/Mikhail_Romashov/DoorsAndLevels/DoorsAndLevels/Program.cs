using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevels
{
    class Program
    {
        static void Main(string[] args)
        {
            DoorsAndLevels game = new DoorsAndLevels();
            Console.WriteLine("Let`s start to game");

            do
            {
                Console.WriteLine("Choose one of the number for next level or \'0\' to previous level:");
                game.Show();
                string resultStr = Console.ReadLine();
                try
                {
                    int result = Convert.ToInt32(resultStr);
                    try
                    {
                        game.CalcLevel(result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"{e.Message}");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"The value '{resultStr}' is not a number. Choose again.");
                }

            } while (true);
        }
    }
}
