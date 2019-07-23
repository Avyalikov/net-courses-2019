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
        private readonly IInputOutputDevice ioDevice;
        private readonly IDoorsNumbersGenerator doorsNumbersGenerator;

        private int[] doors;
        private Stack<int> userDoors;

        public Game(IPhraseProvider m_phraseProvider, IInputOutputDevice m_ioDevice, IDoorsNumbersGenerator m_doorsNumbersGenerator)
        {
            phraseProvider = m_phraseProvider;
            ioDevice = m_ioDevice;
            doorsNumbersGenerator = m_doorsNumbersGenerator;
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
            string Numbers = phraseProvider.GetPhrase("TheNumbersAre");
            for (int i = 0; i < 5; i++)
            {
                Numbers = Numbers + doors[i] + " ";
            }
            Numbers += phraseProvider.GetPhrase("ChooseYourDoor");
            ioDevice.WriteOutput(Numbers);
        }

        private int DoorIsNumber()
        {
            bool isNumber = false;
            int enteredDoor;
            do
            {
                if (int.TryParse(ioDevice.ReadInput(), out enteredDoor))
                {
                    isNumber = true;
                }
                else
                {
                    ioDevice.WriteOutput(phraseProvider.GetPhrase("ItIsNotANumber"));
                }

            } while (!isNumber);
            
            return enteredDoor;
        }


        public void Run()
        {
            ioDevice.WriteOutput(phraseProvider.GetPhrase("Welcome"));
            GenerateDoors();
            userDoors = new Stack<int>();
            bool findTheDoor = false;
            byte level = 1;
            byte maxLevel = 4;
            int door;

            while (true)
            {
                Show();
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
                    ioDevice.WriteOutput(phraseProvider.GetPhrase("TheNextLevel"));
                    level++;
                    userDoors.Push(door);
                    for (int i = 0; i < 5; i++)
                    {
                        doors[i] = doors[i] * door;
                    }
                }
                else if (door == 0 && level > 1)
                {
                    ioDevice.WriteOutput(phraseProvider.GetPhrase("ThePreviousLevel"));
                    level--;
                    door = userDoors.Pop();
                    for (int i = 0; i < 5; i++)
                    {
                        doors[i] = doors[i] / door;
                    }
                }
                else if (door == 0 || level == maxLevel)
                {
                    ioDevice.WriteOutput(phraseProvider.GetPhrase("ThankYouForPlaying"));
                    break;
                }
                else
                {
                    ioDevice.WriteOutput(phraseProvider.GetPhrase("WrongDoor"));
                }

            }
        }
    }
}
