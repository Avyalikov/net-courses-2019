using ConsoleCanvas.Interfaces;
using System.IO;

namespace ConsoleCanvas.Drawers
{
    public class GooseDrawer : IObjectDrawer
    {
        private readonly IDrawManager drawManager;
        private string[] lines;
        private bool isInitialized = false;
        const string filePath = "goose.txt";

        public GooseDrawer(IDrawManager drawManager)
        {
            this.drawManager = drawManager;
        }
        
        public void DrawObject(IBoard board)
        {
            if (!isInitialized)
            {
                InitiateGoose();
            }

            int currentLine = board.y1;
            int linesNumberFromArray;
            int lineLength;

            while (currentLine < (board.y2 - 1) && (currentLine - board.y1) < lines.Length)
            {
                linesNumberFromArray = currentLine - board.y1;
                if (board.BoardSizeY - 1 < lines[currentLine - board.y1].Length)    
                {
                    // if canvas smaller than picture
                    lineLength = board.BoardSizeY - 1;
                    // trim string to fit canvas 
                    drawManager.WriteAt(
                        lines[linesNumberFromArray].Substring(0, lineLength),   
                        board.x1 + 1, 
                        currentLine + 1);
                }
                else
                {
                    drawManager.WriteAt(
                    lines[linesNumberFromArray], board.x1 + 1, currentLine + 1);
                }
                currentLine++;
            }
        }

        private void InitiateGoose()
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            lines = File.ReadAllLines(filePath);

            isInitialized = true;
        }
    }
}

