namespace ConsoleCanvas.Drawers
{
    using System.IO;
    using ConsoleCanvas.Interfaces;

    public class GooseDrawer : IObjectDrawer
    {
        private const string FilePath = "goose.txt";

        private readonly IDrawManager drawManager;
        private string[] lines;
        private bool isInitialized = false;

        public GooseDrawer(IDrawManager drawManager)
        {
            this.drawManager = drawManager;
        }
        
        public void DrawObject(IBoard board)
        {
            if (!this.isInitialized)
            {
                this.InitiateGoose();
            }

            int currentLine = board.Y1;
            int linesNumberFromArray;
            int lineLength;

            while (currentLine < (board.Y2 - 1) && (currentLine - board.Y1) < this.lines.Length)
            {
                linesNumberFromArray = currentLine - board.Y1;
                if (board.BoardSizeY - 1 < this.lines[currentLine - board.Y1].Length)    
                {
                    // if canvas smaller than picture
                    lineLength = board.BoardSizeY - 1;

                    // trim string to fit canvas 
                    this.drawManager.WriteAt(
                        this.lines[linesNumberFromArray].Substring(0, lineLength),   
                        board.X1 + 1, 
                        currentLine + 1);
                }
                else
                {
                    this.drawManager.WriteAt(
                    this.lines[linesNumberFromArray], board.X1 + 1, currentLine + 1);
                }

                currentLine++;
            }
        }

        private void InitiateGoose()
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException(FilePath);
            }

            this.lines = File.ReadAllLines(FilePath);

            this.isInitialized = true;
        }
    }
}