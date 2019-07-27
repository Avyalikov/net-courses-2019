using System;

namespace DashboardGame
{
    static class Drawer
    {
        static Random rand = new Random();
        public static void DrawPoint(IBoard board)
        {
            int x = rand.Next(-board.GetWidth() / 2 + 2, board.GetWidth() / 2 - 3);
            int y = rand.Next(-board.GetHeight() / 2 + 2, board.GetHeight() / 2 - 1);

            board.SetColor(ConsoleColor.Red);
            board.DrawAtPosition(x, y, "•");
            board.ResetColor();
        }
        public static void DrawVerticalLine(IBoard board)
        {
            int x = rand.Next(-board.GetWidth() / 2 + 1, board.GetWidth() / 2 - 3);
            board.SetColor(ConsoleColor.Yellow);

            for (int i = -board.GetHeight() / 2 + 2; i < board.GetHeight() / 2; i++)
            {
                board.DrawAtPosition(x, i, "|");
            }
            board.ResetColor();
        }
        public static void DrawHorizontalLine(IBoard board)
        {
            int y = rand.Next(-board.GetHeight() / 2 + 2, board.GetHeight() / 2 - 1);
            board.SetColor(ConsoleColor.Cyan);

            for (int i = -board.GetWidth() / 2 + 1; i < board.GetWidth() / 2 - 2; i++)
            {
                board.DrawAtPosition(i, y, "―");
            }
            board.ResetColor();
        }
        public static void DrawParabola(IBoard board)
        {
            board.SetColor(ConsoleColor.DarkMagenta);

            for (int i = -board.GetHeight() / 2 + 1; i < board.GetHeight() / 2 - 3; i++)
            {
                int j = Parabola(i);
                if (j < board.GetHeight() / 2)
                {
                    board.DrawAtPosition(i, j, "•");
                }
            }
            board.ResetColor();
        }
        // y = (x^2)/4
        private static int Parabola(int x) => x * x / 4;
    }
}
