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
        private const Int32 MIN_DOOR_NUMBER = 0;
        private const Int32 MAX_DOOR_NUMBER = 5;

        private Int32[] doors = new Int32[MAX_DOORS];
        private Int32[] level = new Int32[MAX_DOORS];
        private List<Int32> doorsStack = new List<Int32>();

        public DoorsGame()
        {
            Random rand = new Random();

            for (Int32 i = 0; i < doors.Length; i++) //create doors
            {
                Boolean repeat = false;
                do
                {
                    repeat = false;
                    doors[i]= rand.Next(MIN_DOOR_NUMBER, MAX_DOOR_NUMBER);

                    for (Int32 j = 0; j < i; j++){   //check for unique
                        if(doors[j] == doors[i]){
                            repeat = true;          //door isn't unique; need to repeat
                            break;
                        }
                    }
                } while(repeat);
            }

            for (Int32 i = 0; i < doors.Length; i++) //initiate first level with doors
            {
                level[i] = doors[i];    
            }
            
        }


        private Int32 PopLastDoor () 
        {
            Int32 lastDoor = doorsStack[doorsStack.Count - 1];
            doorsStack.RemoveAt(doorsStack.Count - 1);
            return lastDoor;
        }

        private String ShowLevel()
        {
            String levelString = "";
            for (Int32 i = 0; i < level.Length; i++) 
            {
                levelString = levelString + level[i].ToString() + " " ;
            }
            return levelString;
        }

        private void ExecuteTheDoor(Int32 door)
        {
            if(door == 0)
            {
                if(doorsStack.Count>0)
                {
                    Int32 lastDoor = PopLastDoor();
                    for (Int32 i = 0; i < level.Length; i++) 
                    {
                        level[i] /= lastDoor;
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
                doorsStack.Add(door);
                for (Int32 i = 0; i < level.Length; i++) 
                {
                    level[i] *= door;
                }
                Console.Write($"We select number { door } and go to next level: ");
                Console.WriteLine(ShowLevel());
            }
        }

        public void EnterTheDoor(Int32 doorToEnter)
        {
            Boolean doorExist = false;
            for (Int32 i = 0; i < doors.Length; i++) 
            {   
                if(doorToEnter == doors[i])
                {
                    doorExist = true;       
                    break;
                }
            }
            if(doorExist)
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
                Console.Write(doors[i]+" ");
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
            while(true)
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
