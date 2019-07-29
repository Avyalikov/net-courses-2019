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
        private int boardSizeX = 0;
        private int boardSizeY = 0;
        public int ExitCode { get; set; }
        public int NumberOfChoices { get; set; }
        public int BoardSizeX {
            get {
                return boardSizeX;
            }
            set {
                if (value < minX)
                    throw new Exception(
                    $"The game works incorect, if size of board less than 5x5");
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
                if (value < minY)
                    throw new Exception(
                    $"The game works incorect, if size of board less than {minX}x{minY}");
                boardSizeY = value;
            }
        }
        public string Language = string.Empty;
    }
}
