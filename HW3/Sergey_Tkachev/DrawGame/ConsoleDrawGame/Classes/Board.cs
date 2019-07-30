using ConsoleDrawGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawGame.Classes
{
    class Board : IBoard
    {
        private readonly ConsoleInputOutput cio;
        private readonly GameSettings gameSettings;

        public int boardSizeX { get; set; }
        public int boardSizeY { get; set; }

        public Board(IInputOutput concoleInputOutput, ISettingsProvider settingsProvider)
        {
            this.cio = (ConsoleInputOutput) concoleInputOutput;
            this.gameSettings = settingsProvider.GetGameSettings();
            this.boardSizeX = gameSettings.BoardSizeX;
            this.boardSizeY = gameSettings.BoardSizeY;
        }

        public void PrintBoard()
        {
            cio.SetCursor(gameSettings.StartPointX, gameSettings.StartPointY);
            // Draw the left side of a (ex.: 10x10) rectangle, from top to bottom.
            cio.WriteAt("+", 0, 0);
            for (int i = 1; i < boardSizeY - 1; i++)
            {
                cio.WriteAt("|", 0, i);
            }
            cio.WriteAt("+", 0, boardSizeY - 1);

            // Draw the bottom side, from left to right.
            for (int i = 1; i < boardSizeX - 1; i++) // shortcut: WriteAt("---", 1, 9)
            {
                cio.WriteAt("-", i, boardSizeY - 1);
            }
            cio.WriteAt("+", boardSizeX - 1, boardSizeY - 1);

            // Draw the right side, from bottom to top.
            for (int i = boardSizeY - 2; i > 0; i--) // shortcut: WriteAt("---", 9, 8)
            {
                cio.WriteAt("|", boardSizeX - 1, i);
            }
            cio.WriteAt("+", boardSizeX - 1, 0);

            // Draw the top side, from right to left.
            for (int i = boardSizeX - 2; i > 0; i--) // shortcut: WriteAt("---", 8, 0)
            {
                cio.WriteAt("-", i, 0);
            }
            cio.SetCursor(0, gameSettings.StartPointY + boardSizeY + 1);
        }

        public void PrintDot()
        {
            double x = boardSizeX;
            double y = boardSizeY;
            cio.SetCursor(gameSettings.StartPointX + (int)Math.Floor(x * 0.25), gameSettings.StartPointY + (int)Math.Floor(y * 0.3));

            cio.WriteAt(".", 0, 0);
            cio.SetCursor(0, gameSettings.StartPointY + boardSizeY + 1);
        }

        public void PrintHorizontal()
        {
            double x = boardSizeX;
            double y = boardSizeY;
            cio.SetCursor(gameSettings.StartPointX + (int)Math.Ceiling(x * 0.55), gameSettings.StartPointY + (int)Math.Ceiling(y * 0.3));
            for (int i = 0; i < (boardSizeX - (int)Math.Ceiling(x * 0.55) - 1); i++)
            {
                cio.WriteAt("-", i, 0);
            }
            cio.SetCursor(0, gameSettings.StartPointY + boardSizeY + 1);
        }

        public void PrintOtherCurve()
        {
            double x = boardSizeX;
            double y = boardSizeY;
            bool invert = false;
            cio.SetCursor(gameSettings.StartPointX + (int)Math.Ceiling(x * 0.55), gameSettings.StartPointY + (int)Math.Ceiling(y * 0.55));
            for (int i = 0; i < (boardSizeX - (int)Math.Ceiling(x * 0.55) - 1); i++)
            {
                if (i % 2 == 0)
                    invert = !invert;
                if(invert)
                    cio.WriteAt("/", i, -i % 2);
                else
                    cio.WriteAt("\\", i, -1 + i % 2);
            }
            cio.SetCursor(0, gameSettings.StartPointY + boardSizeY + 1);
        }

        public void PrintVertical()
        {
            double x = boardSizeX;
            double y = boardSizeY;
            cio.SetCursor(gameSettings.StartPointX + (int)Math.Floor(x * 0.55), gameSettings.StartPointY + 0 + 1);
            for (int i = 0; i < boardSizeY - 1 - 1; i++)
            {
                cio.WriteAt("|", 0, i);
            }
            cio.SetCursor(0, gameSettings.StartPointY + boardSizeY + 1);
        }
    }
}
