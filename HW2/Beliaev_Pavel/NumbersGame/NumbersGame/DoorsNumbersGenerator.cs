using System;
using NumbersGame.Interfaces;

namespace NumbersGame
{
    public class DoorsNumbersGenerator : IDoorsNumbersGenerator //TODO
    {
        public int[] GenerateDoorsNumbers(int doorsAmount)
        {
            int[] doorsNumbers = new int[doorsAmount];
            int newDoor;
            bool NewIsOld;
            doorsNumbers[0] = 0;
            Random rnd = new Random();
            for(int door = 1; door < doorsAmount;)
            {
                NewIsOld = false;
                newDoor = rnd.Next(1, doorsAmount+5);
                for (int i = 1; i < door ; i++ )
                {
                    if (doorsNumbers[i] == newDoor)
                    {
                        NewIsOld = true;
                        break;
                    }
                }
                if (NewIsOld) continue;

                doorsNumbers[door] = newDoor;

                door++;
            }
            return doorsNumbers;
        }
    }
}
