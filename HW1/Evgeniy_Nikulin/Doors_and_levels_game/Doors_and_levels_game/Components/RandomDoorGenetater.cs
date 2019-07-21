using Doors_and_levels_game.Interfaces;
using System;
using System.Collections.Generic;

namespace Doors_and_levels_game.Components
{
    class RandomDoorGenetater : IDoorsGenerater<List<ulong>>
    {
        Random rnd = new Random();
        public List<ulong> Generate(int n)
        {
            List<ulong> doors = new List<ulong>();
            for (int i = 0; i < n - 1; i++)
            {
                while (true)
                {
                    ulong r = (ulong)rnd.Next(2, 10);
                    if (!doors.Contains(r))
                    {
                        doors.Add(r);
                        break;
                    }
                }
            }
            doors.Add(0);
            return doors;
        }
    }
}