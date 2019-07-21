using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doors_levels
{
    class DoorsGame
    {
        private const Int32 MAX_DOORS = 5;
        private const Int32 MIN_DOOR_VALUE = 1;
        private const Int32 MAX_DOOR_VALUE = 90;
        private const Int32 GET_BACK_NUMBER = 0;

        private Int32[] doors = new Int32[MAX_DOORS];
        private Stack<Int32> levelsStack = new Stack<Int32>();

        public DoorsGame()
        {
            InitiateDoors();
        }


        private void InitiateDoors()
        {
            Random rand = new Random();
            doors[0] = GET_BACK_NUMBER;  //initiating return to previous level ability
            for (Int32 i = 1; i < doors.Length; i++) //create doors
            {
                Boolean repeat = false;
                do
                {
                    repeat = false;
                    doors[i] = rand.Next(MIN_DOOR_VALUE, MAX_DOOR_VALUE);

                    for (Int32 j = 0; j < i; j++)
                    {   //check for unique
                        if (doors[j] == doors[i])
                        {
                            repeat = true;          //door isn't unique; need to repeat
                            break;
                        }
                    }
                } while (repeat);
            }
        }


        private String ShowLevel()
        {
            String levelString = "";
            for (Int32 i = 0; i < doors.Length; i++)
            {
                levelString = levelString + doors[i].ToString() + " ";
            }
            return levelString;
        }

        private void ExecuteTheDoor(Int32 currentDoor)
        {
            if (currentDoor == 0)
            {
                if (levelsStack.Count > 0)
                {
                    Int32 lastDoor = levelsStack.Pop();
                    for (Int32 i = 0; i < doors.Length; i++)
                    {
                        doors[i] /= lastDoor;
                    }
                    Console.Write("We select number 0 and go to previous level: ");
                    Console.WriteLine(ShowLevel());
                }
                else
                {
                    Console.WriteLine("It's first level. Cant get higher.");
                }
            }
            else //if door != 0
            {
                levelsStack.Push(currentDoor);
                for (Int32 i = 0; i < doors.Length; i++)
                {
                    try
                    {
                        checked
                        {
                            doors[i] *= currentDoor;
                        }
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("You get too far! Droped to first level");
                        InitiateDoors();
                        levelsStack.Clear();
                        ShowDoors();
                        return;
                    }
                }
                Console.Write($"We select number { currentDoor } and go to next level: ");
                Console.WriteLine(ShowLevel());
            }
        }

        public void EnterTheDoor(Int32 doorToEnter)
        {
            Boolean doorExist = false;
            for (Int32 i = 0; i < doors.Length; i++)
            {
                if (doorToEnter == doors[i])
                {
                    doorExist = true;
                    break;
                }
            }
            if (doorExist)
            {
                ExecuteTheDoor(doorToEnter);
            }
            else
            {
                Console.WriteLine("Door doesn't exist");
            }

        }

        public void ShowDoors()
        {
            Console.Write("We have numbers: ");
            for (Int32 i = 0; i < doors.Length; i++)
            {
                Console.Write(doors[i] + " ");
            }
            Console.WriteLine("");
        }

    }




    class Program
    {

        static void Main(string[] args)
        {
            DoorsGame doorsGame = new DoorsGame();
            doorsGame.ShowDoors();
            while (true)
            {
                try
                {
                    int door = Convert.ToInt32(Console.ReadLine());
                    doorsGame.EnterTheDoor(door);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
