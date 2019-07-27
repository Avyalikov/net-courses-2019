namespace DashboardGame
{
    class GameMenu
    {
        public delegate void Draw(IBoard board);
        public Draw DrawFigures;

        private IBoard board;
        public GameMenu(IBoard board)
        {
            this.board = board;
        }
        public void ShowMenu()
        {
            board.DrawAtPosition(-15, 10, "Добро пожаловать в игру!");
            board.DrawAtPosition(-20, 9, "Здесь вы можете построить 4 элемента:");
            board.DrawAtPosition(-20, 8, "1 - Точка");
            board.DrawAtPosition(-20, 7, "2 - Вертикальная линия");
            board.DrawAtPosition(-20, 6, "3 - Горизонтальная линия");
            board.DrawAtPosition(-20, 5, "4 - Парабола");
            board.DrawAtPosition(-25, 4, "Вы можете выбрать элементы просто перечислив их.");
            board.DrawAtPosition(-25, 3, "Например, \"124\" построит точку, вертикальную линию");
            board.DrawAtPosition(-25, 2, "и параболу. Для возврата в меню нажмите \"E\".");
            board.DrawAtPosition(-25, 1, "Выберите элементы: ");
        }
        public string SetUserChoice()
        {
            board.DrawAtPosition(-25 + 19, 1, string.Empty);
            return board.ReadLine();
        }
        public bool ShowUserChoice(string choice)
        {
            board.Clear();
            int y = 9;
            board.DrawAtPosition(-20, y--, "Ваш выбор: " + choice);
            bool isCorrect = false;
            if (choice.Contains("1"))
            {
                board.DrawAtPosition(-20, y--, "1 - Точка");
                
                DrawFigures += Drawer.DrawPoint;
                isCorrect = true;
            }
            if (choice.Contains("2"))
            {
                board.DrawAtPosition(-20, y--, "2 - Вертикальная линия");
                DrawFigures += Drawer.DrawVerticalLine;
                isCorrect = true;
            }
            if (choice.Contains("3"))
            {
                board.DrawAtPosition(-20, y--, "3 - Горизонтальная линия");
                DrawFigures += Drawer.DrawHorizontalLine;
                isCorrect = true;
            }
            if (choice.Contains("4"))
            {
                board.DrawAtPosition(-20, y--, "4 - Парабола");
                DrawFigures += Drawer.DrawParabola;
                isCorrect = true;
            }
            if (isCorrect)
            {
                board.DrawAtPosition(-20, y--, "Нажмите любую клавишу, чтобы построить.");
            }
            if (!isCorrect)
            {
                board.DrawAtPosition(-25, y--, "В вашем выборе нет подходящих значений, попробуйте снова: ");
            }

            board.ReadLine();
            return isCorrect;
        }
    }
}
