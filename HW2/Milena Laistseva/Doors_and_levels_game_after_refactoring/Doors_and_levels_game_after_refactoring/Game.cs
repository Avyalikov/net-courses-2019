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
        private readonly ISettingsProvider settingsProvider;

        private readonly GameSettings gameSettings;

        private int[] doors;
        private Stack<int> userDoors;

        public Game(
            IPhraseProvider m_phraseProvider, 
            IInputOutputDevice m_ioDevice, 
            IDoorsNumbersGenerator m_doorsNumbersGenerator,
            ISettingsProvider m_settingsProvider)
        {
            phraseProvider = m_phraseProvider;
            ioDevice = m_ioDevice;
            doorsNumbersGenerator = m_doorsNumbersGenerator;
            settingsProvider = m_settingsProvider;

            gameSettings = settingsProvider.GetGameSettings();
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

        private int UserInput()
        {
            int result = -1;
            int enteredDoor;
            do
            {
                var input = ioDevice.ReadInput();
                if (int.TryParse(input, out enteredDoor))
                {
                   result = 1;
                }
                else if(input.ToLowerInvariant().Equals(gameSettings.ExitCode))
                {
                    ioDevice.WriteOutput(phraseProvider.GetPhrase("ThankYouForPlaying"));
                    result = 0;
                }
                else
                {
                    ioDevice.WriteOutput(phraseProvider.GetPhrase("ItIsNotANumber"));
                }

            } while (result<0);
          
            if(result == 0)
            {
                enteredDoor = -1;
            }
            return enteredDoor;
        }


        public void Run()
        {
            ioDevice.WriteOutput(phraseProvider.GetPhrase("Welcome"));
            doors = doorsNumbersGenerator.GenerateDoorsNumbers(gameSettings.DoorsAmount);
            userDoors = new Stack<int>();
            bool findTheDoor = false;
            int level = 1;
            int maxLevel = gameSettings.MaxLevel;
            int door;

            while (true)
            {
                Show();
                door = UserInput();
                findTheDoor = false;

                if(door < 0)
                {
                    break;
                }

                    for (int i = 0; i < (gameSettings.DoorsAmount - 1); i++)
                    {
                        if (door == doors[i])
                        {
                            findTheDoor = true;
                        }
                    }

                    if (level < maxLevel && findTheDoor && door != gameSettings.ExitDoorNumber)
                    {
                        ioDevice.WriteOutput(phraseProvider.GetPhrase("TheNextLevel"));
                        level++;
                        userDoors.Push(door);
                        for (int i = 0; i < 5; i++)
                        {
                            doors[i] = doors[i] * door;
                        }
                    }
                    else if (door == gameSettings.ExitDoorNumber && level > 1)
                    {
                        ioDevice.WriteOutput(phraseProvider.GetPhrase("ThePreviousLevel"));
                        level--;
                        door = userDoors.Pop();
                        for (int i = 0; i < 5; i++)
                        {
                            doors[i] = doors[i] / door;
                        }
                    }
                    else if (door == gameSettings.ExitDoorNumber || level == maxLevel)
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
