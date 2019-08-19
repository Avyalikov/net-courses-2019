namespace Core.Model
{
    public struct Point
    {
        public int x;
        public int y;

        public static implicit operator Point((int, int) value) =>
              new Point { x = value.Item1, y = value.Item2 };
    }
}