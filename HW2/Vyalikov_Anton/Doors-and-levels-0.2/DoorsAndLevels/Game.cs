using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DoorsAndLevels
{
    class Game
    {
        //Stack is using to store all chosen doors.
        private Stack<int> chosenDoors = new Stack<int>();

        //This list is using for store current doors, that user can choose.
        List<int> doorsNumbers = new List<int>();

        private readonly Interfaces.IDoorsGenerator doorsGenerator;
        private readonly Interfaces.IInputOutputModule ioModule;
        private readonly Interfaces.IPhraseProvider phraseProvider;
        private readonly Interfaces.ISettingsProvider settingsProvider;

        public Game(Interfaces.IDoorsGenerator doorsGenerator,
            Interfaces.IInputOutputModule ioModule,
            Interfaces.IPhraseProvider phraseProvider,
            Interfaces.ISettingsProvider settingsProvider)
        {
            this.doorsGenerator = doorsGenerator;
            this.ioModule = ioModule;
            this.phraseProvider = phraseProvider;
            this.settingsProvider = settingsProvider;
        }

        //Main loop
        public void Start(int doorsNum)
        {
            settingsProvider.ParseXML();
            phraseProvider.ParseXML(settingsProvider.GetSetting("Language"));

            ioModule.WriteOutput(phraseProvider.GetMessage("Start"));

            int amountOfDoors = Convert.ToInt32(settingsProvider.GetSetting("DoorsCount"));
            int minDoorNum = Convert.ToInt32(settingsProvider.GetSetting("MinRandom"));
            int maxDoorNum = Convert.ToInt32(settingsProvider.GetSetting("MaxRandom"));

            if (maxDoorNum - minDoorNum > amountOfDoors)
            {
                doorsNumbers = doorsGenerator.GetDoorsNumbers(amountOfDoors, minDoorNum, maxDoorNum);
                PrintList(doorsNumbers);
            }

            else
            {
                throw new Exception ("Incorrect settings. Amount of door less, than should be.");
            }
            

            ioModule.WriteOutput(phraseProvider.GetMessage("ExitCommand") + "\n");

            string door;
            int curDoor;

            while (true)
            {
                door = ioModule.ReadInput();

                if (door.ToLower().Equals("exit"))
                {
                    ioModule.WriteOutput(phraseProvider.GetMessage("Exit"));
                    break;
                }

                try
                {
                    curDoor = Convert.ToInt32(door);

                    if (curDoor == 0)
                    {
                        PreviousLevel();
                    }
                     
                    else if (doorsNumbers.Contains(curDoor))
                    {
                        NextLevel(curDoor);
                    }

                    else
                    {
                        ioModule.WriteOutput(phraseProvider.GetMessage("IncorrectNumber"));
                    }
                }

                catch (FormatException)
                {
                    ioModule.WriteOutput(phraseProvider.GetMessage("InvalidCommand"));
                }

                catch (OverflowException)
                {
                    ioModule.WriteOutput(phraseProvider.GetMessage("Overflow"));
                }
            }
        }

        //Method is using for return to the previous level.
        private void PreviousLevel()
        {
            if (chosenDoors.Count > 0)
            {
                int previousDoor = chosenDoors.Pop();
                for (int i = 0; i < doorsNumbers.Count; i++)
                {
                    doorsNumbers[i] = doorsNumbers[i] / previousDoor;
                }
                ioModule.WriteOutput(phraseProvider.GetMessage("LevelDown"));
            }
            
            else
            {
                ioModule.WriteOutput(phraseProvider.GetMessage("FirstLevel"));
            }
            PrintList(doorsNumbers);
        }

        //Method is using for jump to the next level.
        private void NextLevel(int door)
        {
            chosenDoors.Push(door);
            List<int> prevDoors = new List<int>();
            for (int i = 0; i < doorsNumbers.Count; i++)
            {
                doorsNumbers[i] = doorsNumbers[i] * door;
            }

            if (doorsNumbers.Max() >= 46000)
            {
                ioModule.WriteOutput(phraseProvider.GetMessage("Victory"));
            }

            else
            {
                ioModule.WriteOutput(phraseProvider.GetMessage("LevelUp"));
                PrintList(doorsNumbers);
            }

        }

        //Method is using for printing to console current doors numbers.
        public void PrintList(List<int> lst)
        {
            StringBuilder doors = new StringBuilder();
            foreach (int i in lst)
            {
                doors.Append(i + " ");
            }
            ioModule.WriteOutput($"\n{doors}\n");
        }
    }
}
