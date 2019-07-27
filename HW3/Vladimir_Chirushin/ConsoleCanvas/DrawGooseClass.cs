using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleCanvas
{
    public class DrawGooseClass : IDrawGooseClass
    {
        private readonly IDrawManager drawManager;
        private string[] lines;

        public DrawGooseClass(IDrawManager drawManager)
        {
            this.drawManager = drawManager;
        }
        public void InitiateGoose()
        {
            var list = new List<string>();
            var fileStream = new FileStream(@"goose.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
            lines = list.ToArray();
        }

        public void DrawGoose(Canvas canvas)
        {
            int currentLine = canvas.y1;
            int linesNumberFromArray;
            int lineLength;
            while (currentLine < (canvas.y2-1) && (currentLine - canvas.y1) < lines.Length )
            {
                linesNumberFromArray = currentLine - canvas.y1;
                if ((canvas.y2 - canvas.y1) - 1 < lines[currentLine - canvas.y1].Length)    //if canvas smaller than picture
                {
                    lineLength = (canvas.y2 - canvas.y1) - 1;
                    drawManager.WriteAt(
                    lines[linesNumberFromArray].Substring(0, lineLength),   //trim string to fit canvas 
                    canvas.x1 + 1, currentLine + 1);
                }
                else
                {
                    drawManager.WriteAt(
                    lines[linesNumberFromArray], canvas.x1 + 1, currentLine + 1);
                }              
                currentLine++;
            }
            
        }

    }
}

