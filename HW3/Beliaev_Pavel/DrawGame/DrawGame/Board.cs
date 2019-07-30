namespace DrawGame
{
    using DrawGame.Interfaces;

    public class Board : IBoard
    {
        private int boardSizeX;
        private int boardSizeY;

        public int BoardSizeX
        {
            get => this.boardSizeX;
            set
            {
                if (value < 5 || value > 55)
                {
                    this.boardSizeX = 40;
                }
                else
                {
                    this.boardSizeX = (value / 2) * 2;
                }
            }
        }

        public int BoardSizeY
        {
            get => this.boardSizeY;
            set
            {
                if (value < 5 || value > 35)
                {
                    this.boardSizeY = 20;
                }
                else
                {
                    this.boardSizeY = (value / 2) * 2;
                }
            }
        }        
    }
}
