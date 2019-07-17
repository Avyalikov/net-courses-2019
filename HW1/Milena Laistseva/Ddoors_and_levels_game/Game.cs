using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doors_and_levels_game
{
    public class Game
    {
        private int[] doors;
        private Stack<int> userDoors;

        private void GenerateDoors()
        {
            doors = new int[5];
            Random rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                doors[i] = rand.Next(2, 9);
            }
            doors[4] = 0;
        }


        private void Show()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Write(doors[i] + " ");
            }
            Console.WriteLine();
        }


        public void Run()
        {
            Console.WriteLine("Welcome to the Game of Doors!");
            GenerateDoors();
            userDoors = new Stack<int>();
            bool findTheDoor = false;
            byte level = 1;
            byte maxLevel = 4;
            int door;

            while (true)
            {
                Show();
                Console.WriteLine("Choose your door!");
                door = Convert.ToInt32(Console.ReadLine());
                findTheDoor = false;

                for (int i = 0; i < 5; i++)
                {
                    if (door == doors[i])
                    {
                        findTheDoor = true;
                    }
                }

                if (level < maxLevel && findTheDoor && door != 0)
                {
                    Console.WriteLine("Congratulations! You are on the next level!");
                    level++;
                    userDoors.Push(door);
                    for (int i = 0; i < 5; i++)
                    {
                        doors[i] = doors[i] * door;
                    }
                }
                else if (door == 0 && level > 1)
                {
                    Console.WriteLine("Sorry! You have returned to the previous level!");
                    level--;
                    door = userDoors.Pop();
                    for (int i = 0; i < 5; i++)
                    {
                        doors[i] = doors[i] / door;
                    }
                }
                else if (door == 0 || level == maxLevel)
                {
                    Console.WriteLine("The End!");
                    Console.WriteLine("Thank you for the game!");
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong door! Try again.");
                }

            }
        }
    }
}
