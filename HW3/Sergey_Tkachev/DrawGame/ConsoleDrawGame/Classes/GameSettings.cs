namespace ConsoleDrawGame.Classes
{
    internal class GameSettings
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

        public int BoardSizeX
        {
            get
            {
                return this.boardSizeX;
            }

            set
            {
                if (value < this.minX || value > this.maxX)
                {
                    this.boardSizeX = this.defaultX;
                }

                this.boardSizeX = value;
            }
        }

        public int BoardSizeY
        {
            get
            {
                return this.boardSizeY;
            }

            set
            {
                if (value < this.minY || value > this.maxY)
                {
                    this.boardSizeY = this.defaultY;
                }

                this.boardSizeY = value;
            }
        }

        public string Language { get; set; }
    }
}
