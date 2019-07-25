using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevels
{
    class Game
    {
        //Stack is using to store all chosen doors.
        private Stack<int> chosenDoors = new Stack<int>();

        //This list is using for store current doors, that user can choose.
        List<int> doorsNumbers = new List<int>();

        //This list is using for store doors of the previous level.
        private List<int> prevDoors = new List<int>();

        //Bool needs to stop the game on the max level.
        private bool finish = false;

        private readonly Interfaces.IDoorsGenerator doorsGenerator;
        private readonly Interfaces.IInputOutputModule ioModule;

        public Game(Interfaces.IDoorsGenerator doorsGenerator,
            Interfaces.IInputOutputModule ioModule)
        {
            this.doorsGenerator = doorsGenerator;
            this.ioModule = ioModule;
        }

        //Main loop
        public void Start(int doorsNum)
        {


            ioModule.WriteOutput(@"Welcome to Doors And Levels game.
You can write 'exit' to leave the game or you can choose one of those doors if you want to play:
");

            List<int> doorsNumbers;
            doorsNumbers = doorsGenerator.GetDoorsNumbers(5);
            PrintList(doorsNumbers);

            string door;
            int curDoor;

            while (true)
            {
                door = ioModule.ReadInput();

                if (door.ToLower().Equals("exit"))
                {
                    ioModule.WriteOutput("\nThank you for playing.\n");
                    break;
                }

                try
                {
                    curDoor = Convert.ToInt32(door);

                    if (curDoor == 0 && finish)
                    {
                        finish = false;
                        ioModule.WriteOutput("\nYou return to the previous level.\n");
                        PrintList(doorsNumbers);
                    }

                    else if (curDoor == 0)
                    {
                        PreviousLevel();
                    }

                    else if (doorsNumbers.Contains(curDoor))
                    {
                        NextLevel(curDoor);
                    }

                    else
                    {
                        ioModule.WriteOutput("\nYou wrote incorrect number. Please try again.\n");
                    }
                }

                catch (FormatException)
                {
                    ioModule.WriteOutput("\nInvalid command. Please try again.\n");
                }

                catch (OverflowException)
                {
                    ioModule.WriteOutput("\nYou wrote incorrect door number. Please try again.\n");
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
                ioModule.WriteOutput("\nYou've returned to the previous level.\n");
            }
            
            else
            {
                ioModule.WriteOutput("\nYou are on the first level already.\n");
            }
            PrintList(doorsNumbers);
        }

        //Method is using for jump to the next level.
        private void NextLevel(int door)
        {
            chosenDoors.Push(door);
            prevDoors = doorsNumbers;
            for (int i = 0; i < doorsNumbers.Count; i++)
            {
                if (doorsNumbers[i] * door < 0 || i>0 && doorsNumbers[i] * door == 0)
                {
                    finish = true;
                    doorsNumbers = prevDoors;
                    chosenDoors.Pop();
                    break;
                }

                else
                {
                    doorsNumbers[i] = doorsNumbers[i] * door;
                }

            }

            if (finish)
            {
                ioModule.WriteOutput("\nYou've reached max level of the game.\nYou can print 0 to return to the previous level, or write exit to leave the game.\n");
            }

            else
            {
                ioModule.WriteOutput("\nYou've moved to the next level.\nPlease choose one of those doors:\n");
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
            ioModule.WriteOutput($"{doors}\n");
        }

    }
}
