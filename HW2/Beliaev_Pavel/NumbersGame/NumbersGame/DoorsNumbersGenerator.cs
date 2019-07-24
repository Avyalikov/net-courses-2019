using System;
using NumbersGame.Interfaces;

namespace NumbersGame
{
    public class DoorsNumbersGenerator : IDoorsNumbersGenerator //TODO
    {
        public int[] GenerateDoorsNumbers(int doorsAmount)
        {
            int[] doorsNumbers = new int[doorsAmount];
            doorsNumbers[0] = 0;
            Random rnd = new Random();
            for(int door = 1; door < doorsAmount; door++)
            {
                door = rnd.Next(1, 9);
            }
            return doorsNumbers;
        }
    }
}
