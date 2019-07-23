using System;

namespace doors_levels
{
    public class DoorsGenerator : IDoorsGenerator
    {
        private Int32 minDoorValue;
        private Int32 maxDoorValue;
        private Int32 getBackNumber;

        public DoorsGenerator(Int32 minDoorValue, Int32 maxDoorValue, Int32 getBackNumber)
        {
            this.minDoorValue = minDoorValue;
            this.maxDoorValue = maxDoorValue;
            this.getBackNumber = getBackNumber;
        }

        public int[] GetDoors(int doorsAmount)
        {
            Random rand = new Random();
            Int32[] doors = new Int32[doorsAmount];

            doors[0] = getBackNumber;  //initiating return to previous level ability
            for (Int32 i =   1; i < doors.Length; i++) //create doors
            {
                Boolean repeat = false;
                do
                {
                    repeat = false;
                    doors[i] = rand.Next(minDoorValue, maxDoorValue);

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
