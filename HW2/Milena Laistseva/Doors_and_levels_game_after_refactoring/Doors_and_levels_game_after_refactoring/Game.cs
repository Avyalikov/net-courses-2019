using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doors_and_levels_game_after_refactoring
{
    class Game
    {
        private readonly IPhraseProvider phraseProvider;

        private int[] doors;
        private Stack<int> userDoors;

        public Game(IPhraseProvider m_phraseProvider)
        {
            phraseProvider = m_phraseProvider;
        }

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

        private int DoorIsNumber()
        {
            bool isNumber = false;
            int enteredDoor;
            do
            {
                if (int.TryParse(Console.ReadLine(), out enteredDoor))
                {
                    isNumber = true;
                }
                else
                {
                    // Console.WriteLine("It is not a number. Let's try again!");
                    Console.WriteLine(phraseProvider.GetPhrase("ItIsNotANumber"));
                }

            } while (!isNumber);
            
            return enteredDoor;
        }


        public void Run()
        {
            //Console.WriteLine("Welcome to the Game of Doors!");
            Console.WriteLine(phraseProvider.GetPhrase("Welcome"));
            GenerateDoors();
            userDoors = new Stack<int>();
            bool findTheDoor = false;
            byte level = 1;
            byte maxLevel = 4;
            int door;

            while (true)
            {
                //Console.WriteLine("The numbers are:");
                Console.WriteLine(phraseProvider.GetPhrase("TheNumbersAre"));
                Show();
                // Console.WriteLine("Choose your door!");
                Console.WriteLine(phraseProvider.GetPhrase("ChooseYourDoor"));
                door = DoorIsNumber();
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
                    //Console.WriteLine("Congratulations! You are on the next level!");
                    Console.WriteLine(phraseProvider.GetPhrase("TheNextLevel"));
                    level++;
                    userDoors.Push(door);
                    for (int i = 0; i < 5; i++)
                    {
                        doors[i] = doors[i] * door;
                    }
                }
                else if (door == 0 && level > 1)
                {
                    // Console.WriteLine("Sorry! You have returned to the previous level!");
                    Console.WriteLine(phraseProvider.GetPhrase("ThePreviousLevel"));
                    level--;
                    door = userDoors.Pop();
                    for (int i = 0; i < 5; i++)
                    {
                        doors[i] = doors[i] / door;
                    }
                }
                else if (door == 0 || level == maxLevel)
                {
                    //Console.WriteLine("The End!");
                    // Console.WriteLine("Thank you for the game!");
                    Console.WriteLine(phraseProvider.GetPhrase("ThankYouForPlaying"));
                    break;
                }
                else
                {
                    //Console.WriteLine("Wrong door! Try again.");
                    Console.WriteLine(phraseProvider.GetPhrase("WrongDoor"));
                }

            }
        }
    }
}
