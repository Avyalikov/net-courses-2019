using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevelsGame_1
{
    class Game
    {
        public int[] doorNumbersArray = new int[5];

        public List<int> levelDoorNumberArray = new List<int>() {1};

        public int userDoorSelect;

        public Game()
        {
            Program program = new Program();
            program.Introduction();
            doorNumbersArray = program.RandomNumberGenerator();
        }
    }
}
