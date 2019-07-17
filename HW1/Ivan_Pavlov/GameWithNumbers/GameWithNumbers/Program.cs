using System;

namespace GameWithNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            EngGame LetsPlay = new EngGame();

            while (true)
            {
                try
                {
                    LetsPlay.Play(int.Parse(Console.ReadLine()));
                }
                catch (FormatException e)
                {
                    Console.WriteLine("You lose. {0}", e.Message);
                    Console.WriteLine("Next try");
                    LetsPlay = new EngGame();
                }
            }
        }
    }
}
