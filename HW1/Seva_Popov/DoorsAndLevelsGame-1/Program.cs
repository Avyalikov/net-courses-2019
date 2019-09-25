using System;
using System.Linq;

namespace DoorsAndLevelsGame_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.RunGame();
        }

        private void RunGame()
        {
            Game game = new Game();
            Console.WriteLine(" ");

            OutputNumbersToConsole(game.doorNumbersArray);
            Console.WriteLine(" ");

            while (true)
            {
                try
                {
                    Console.WriteLine();

                    game.userDoorSelect = int.Parse(Console.ReadLine());

                    if (game.userDoorSelect == 777)
                    {
                        break;
                    }
                    ReadAndCheckLine(game.userDoorSelect, game);

                    Console.WriteLine("**********************");

                    OutputNumbersToConsole(game.doorNumbersArray);
                }
                catch (System.FormatException)
                {
                    Console.WriteLine(" You have entered something wrong !!!");
                }
            }
        }

        public int[] RandomNumberGenerator()
        {
            int[] doorNumbers = new int[5];
            Random random = new Random();

            for (int i = 0; i < doorNumbers.Length; i++)
            {
                doorNumbers[i] = random.Next(1, 9);
            }

            doorNumbers[doorNumbers.Length - 1] = 0;

            return doorNumbers;
        }

        public void Introduction()
        {
            Console.WriteLine("*****************************************************************************************************************");
            Console.WriteLine(@"This is game ""Doors and levels game" +
                "You can go to a next level by choosing and entering one of the given numbers '1' or go to a previous level by entering '0'\n" +
                "You can quit the game by entering '777' in the console");
            Console.WriteLine("*****************************************************************************************************************");
        }

        private void OutputNumbersToConsole(int[] doorNumbers)
        {
            foreach (var item in doorNumbers)
            {
                Console.Write(item + " ");
            }
        }

        private void ReadAndCheckLine(int userDoorSelect, Game game)
        {
            if (game.doorNumbersArray.Contains(userDoorSelect))
            {
                if (userDoorSelect != 0)
                {
                    game.levelDoorNumberArray.Add(userDoorSelect);
                    game.doorNumbersArray = LevelUp(game.doorNumbersArray, userDoorSelect);
                }

                if (userDoorSelect == 0)
                {
                    LevelDown(game.doorNumbersArray, game.levelDoorNumberArray[game.levelDoorNumberArray.Count - 1]);

                    if (game.levelDoorNumberArray.Count > 1)
                    {
                        game.levelDoorNumberArray.RemoveAt(game.levelDoorNumberArray.Count - 1);
                    }
                }
            } else
	                {
                        Console.WriteLine(" You entered some wrong number !!!");
	                }         
        }

        private int[] LevelUp(int[] doorNumbersArray, int userDoorSelect)
        {
            for (int i = 0; i < doorNumbersArray.Length; i++)
            {
                doorNumbersArray[i] *= userDoorSelect;
            }
            return doorNumbersArray;
        }

        private int[] LevelDown(int[] doorNumbersArray, int userDoorSelect)
        {
            for (int i = 0; i < doorNumbersArray.Length; i++)
            {
                doorNumbersArray[i] /= userDoorSelect;
            }
            return doorNumbersArray;
        }
    }
}
