using System;

namespace doors_levels
{
    public class DoorsGenerator : IDoorsGenerator
    {
        private const Int32 MIN_DOOR_VALUE = 1;
        private const Int32 MAX_DOOR_VALUE = 90;
        private const Int32 GET_BACK_NUMBER = 0;

        public int[] GetDoors(int doorsAmount)
        {
            Random rand = new Random();
            Int32[] doors = new Int32[doorsAmount];

            doors[0] = GET_BACK_NUMBER;  //initiating return to previous level ability
            for (Int32 i =   1; i < doors.Length; i++) //create doors
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
            return doors;
        }
    }
}
