namespace ConsoleDraw.Provider
{
    using ConsoleDraw.Interfaces;

    class Board : IBoard
    {
        private readonly char angle = '+';
        private readonly char vertical = '|';
        private readonly char horizontal = '-';

        public int BoardSizeX { get; set; }
        public int BoardSizeY { get; set; }

        private ConsoleIO console = new ConsoleIO();

        public void Create()
        {

            this.WriteAt(this.angle, 0, 0);
            this.WriteAt(this.angle, 0, this.BoardSizeY - 1);
            this.WriteAt(this.angle, this.BoardSizeX - 1, 0);
            this.WriteAt(this.angle, this.BoardSizeX - 1, this.BoardSizeY - 1);

            for (int i = 1; i < this.BoardSizeX - 1; i++)
            {
                this.WriteAt(this.horizontal, i, 0);
                this.WriteAt(this.horizontal, i, this.BoardSizeY - 1)
;           }

            for (int i = 1; i < this.BoardSizeY - 1; i++)
            {
                this.WriteAt(this.vertical, 0, i);
                this.WriteAt(this.vertical, this.BoardSizeX - 1, i);
            }
        }

        public void WriteAt(char c, int x, int y)
        {
           //try?
            console.SetCursorPosition(x, y);
            console.WriteOutput(c.ToString());

        }
    }
}
