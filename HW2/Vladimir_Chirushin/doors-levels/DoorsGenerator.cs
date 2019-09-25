using System;

namespace doors_levels
{
    public class DoorsGenerator : IDoorsGenerator
    {
        private Int32 minDoorValue;
        private Int32 maxDoorValue;
        private const Int32 infinityLoopProtectionMaxValue = 100000;

        public DoorsGenerator(Int32 minDoorValue, Int32 maxDoorValue)
        {
            this.minDoorValue = minDoorValue;
            this.maxDoorValue = maxDoorValue;
        }

        public int[] GetDoors(int doorsAmount)
        {
            Random rand = new Random();
            Int32[] doors = new Int32[doorsAmount];

            doors[0] = 0;  //initiating return to previous level ability only works with "0" iteam
            for (Int32 i =   1; i < doors.Length; i++) //create doors
            {
                Boolean repeat = false;
                Int32 infinityLoopProtection = 0;
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

                    infinityLoopProtection++;
                    if(infinityLoopProtection > infinityLoopProtectionMaxValue)
                    {
                        throw new Exception("Infinity loop in doors generator. Check game Settings.");
                    }
                } while (repeat);
            }
            return doors;
        }
    }
}
