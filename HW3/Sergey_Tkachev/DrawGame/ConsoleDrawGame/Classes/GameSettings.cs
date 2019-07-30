using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawGame.Classes
{
    class GameSettings
    {
        private int minX = 5;
        private int minY = 5;
        private int maxX = 100;
        private int maxY = 28;
        private int defaultX = 30;
        private int defaultY = 10;
        private int boardSizeX = 0;
        private int boardSizeY = 0;
        public int StartPointX { get; set; }
        public int StartPointY { get; set; }
        public int ExitCode { get; set; }
        public int NumberOfChoices { get; set; }
        public int BoardSizeX {
            get {
                return boardSizeX;
            }
            set {
                if (value < minX || value > maxX)
                    boardSizeX = defaultX;
                boardSizeX = value;
            }
        }
        public int BoardSizeY {
            get
            {
                return boardSizeY;
            }
            set
            {
                if (value < minY || value > maxY)
                    boardSizeY = defaultY; ;
                boardSizeY = value;
            }
        }
        public string Language = string.Empty;
    }
}
