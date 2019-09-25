using System;

namespace TradingSimulator.Core.Dto
{
    public struct Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static implicit operator Point((int, int) value) =>
              new Point { x = value.Item1, y = value.Item2 };
    }
}