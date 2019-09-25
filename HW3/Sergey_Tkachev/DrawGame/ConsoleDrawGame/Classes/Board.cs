namespace ConsoleDrawGame.Classes
{
    using System;
    using ConsoleDrawGame.Interfaces;

    internal class Board : IBoard
    {
        private readonly ConsoleInputOutput cio;
        private readonly GameSettings gameSettings;

        public Board(IInputOutput concoleInputOutput, ISettingsProvider settingsProvider)
        {
            this.cio = (ConsoleInputOutput)concoleInputOutput;
            this.gameSettings = settingsProvider.GetGameSettings();
            this.BoardSizeX = this.gameSettings.BoardSizeX;
            this.BoardSizeY = this.gameSettings.BoardSizeY;
        }

        public int BoardSizeX { get; set; }

        public int BoardSizeY { get; set; }

        public void PrintBoard()
        {
            this.cio.SetCursor(this.gameSettings.StartPointX, this.gameSettings.StartPointY);
            //// Draw the left side of a (ex.: 10x10) rectangle, from top to bottom.
            this.cio.WriteAt("+", 0, 0);
            for (int i = 1; i < this.BoardSizeY - 1; i++)
            {
                this.cio.WriteAt("|", 0, i);
            }

            this.cio.WriteAt("+", 0, this.BoardSizeY - 1);

            //// Draw the bottom side, from left to right.
            //// shortcut: WriteAt("---", 1, 9)
            for (int i = 1; i < this.BoardSizeX - 1; i++) 
            {
                this.cio.WriteAt("-", i, this.BoardSizeY - 1);
            }

            this.cio.WriteAt("+", this.BoardSizeX - 1, this.BoardSizeY - 1);

            //// Draw the right side, from bottom to top.
            //// shortcut: WriteAt("---", 9, 8)
            for (int i = this.BoardSizeY - 2; i > 0; i--) 
            {
                this.cio.WriteAt("|", this.BoardSizeX - 1, i);
            }

            this.cio.WriteAt("+", this.BoardSizeX - 1, 0);

            //// Draw the top side, from right to left.
            //// shortcut: WriteAt("---", 8, 0)
            for (int i = this.BoardSizeX - 2; i > 0; i--) 
            {
                this.cio.WriteAt("-", i, 0);
            }

            this.cio.SetCursor(0, this.gameSettings.StartPointY + this.BoardSizeY + 1);
        }

        public void PrintDot()
        {
            double x = this.BoardSizeX;
            double y = this.BoardSizeY;
            this.cio.SetCursor(this.gameSettings.StartPointX + (int)Math.Floor(x * 0.25), this.gameSettings.StartPointY + (int)Math.Floor(y * 0.3));

            this.cio.WriteAt(".", 0, 0);
            this.cio.SetCursor(0, this.gameSettings.StartPointY + this.BoardSizeY + 1);
        }

        public void PrintHorizontal()
        {
            double x = this.BoardSizeX;
            double y = this.BoardSizeY;
            this.cio.SetCursor(this.gameSettings.StartPointX + (int)Math.Ceiling(x * 0.55), this.gameSettings.StartPointY + (int)Math.Ceiling(y * 0.3));
            for (int i = 0; i < (this.BoardSizeX - (int)Math.Ceiling(x * 0.55) - 1); i++)
            {
                this.cio.WriteAt("-", i, 0);
            }

            this.cio.SetCursor(0, this.gameSettings.StartPointY + this.BoardSizeY + 1);
        }

        public void PrintOtherCurve()
        {
            double x = this.BoardSizeX;
            double y = this.BoardSizeY;
            bool invert = false;
            this.cio.SetCursor(this.gameSettings.StartPointX + (int)Math.Ceiling(x * 0.55), this.gameSettings.StartPointY + (int)Math.Ceiling(y * 0.55));
            for (int i = 0; i < (this.BoardSizeX - (int)Math.Ceiling(x * 0.55) - 1); i++)
            {
                if (i % 2 == 0)
                {
                    invert = !invert;
                }

                if (invert)
                {
                    this.cio.WriteAt("/", i, -i % 2);
                }
                else
                {
                    this.cio.WriteAt("\\", i, -1 + (i % 2));
                }
            }

            this.cio.SetCursor(0, this.gameSettings.StartPointY + this.BoardSizeY + 1);
        }

        public void PrintVertical()
        {
            double x = this.BoardSizeX;
            this.cio.SetCursor(this.gameSettings.StartPointX + (int)Math.Floor(x * 0.55), this.gameSettings.StartPointY + 0 + 1);
            for (int i = 0; i < this.BoardSizeY - 1 - 1; i++)
            {
                this.cio.WriteAt("|", 0, i);
            }

            this.cio.SetCursor(0, this.gameSettings.StartPointY + this.BoardSizeY + 1);
        }
    }
}
