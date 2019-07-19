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
                game.userDoorSelect = ReadAndCheckLine(Console.ReadLine());

                Console.WriteLine("**********************");

                if (game.userDoorSelect >= 0 && game.userDoorSelect != 10)
                {
                    if (game.userDoorSelect != 0)
                    {
                        game.levelDoorNumberArray.Add(game.userDoorSelect);
                    }

                    if (game.doorNumbersArray.Contains(game.userDoorSelect) && game.userDoorSelect != 0)
                    {
                        game.doorNumbersArray = LevelUp(game.doorNumbersArray, game.userDoorSelect);
                    }

                    if (game.userDoorSelect == 0)
                    {
                        LevelDown(game.doorNumbersArray, game.levelDoorNumberArray[game.levelDoorNumberArray.Count - 1]);

                        if (game.levelDoorNumberArray.Count > 1)
                        {
                            game.levelDoorNumberArray.RemoveAt(game.levelDoorNumberArray.Count - 1);
                        }
                    }

                    if (game.userDoorSelect == 777)
                    {
                        Console.WriteLine("End...");
                        
                        break;
                    }

                    OutputNumbersToConsole(game.doorNumbersArray);
                    Console.WriteLine(" ");
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

            doorNumbers[doorNumbers.Length -1] = 0;

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
            foreach (var item in doorNumbers )
            {
                Console.Write(item + " ");
            }
        }

        private int ReadAndCheckLine(string userDoorSelect)
        {
            try
            {
                int a = int.Parse(userDoorSelect);
                return a;
            }
            catch (System.FormatException)
            {
                Console.WriteLine(" You have entered something wrong !!!");
                return 10;
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
