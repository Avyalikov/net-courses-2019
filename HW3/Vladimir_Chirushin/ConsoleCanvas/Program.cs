using System;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCanvas
{
    public struct Canvas
    {
        public int x1;  //upper left corner
        public int y1;

        public int x2;  //bottom right corner
        public int y2;
    }

    public delegate void DrawDelegate(Canvas canvas);
    class Program
    {

        public static Canvas GetCanvas()
        {
            Canvas canvas = new Canvas();
            canvas.x1 = 3;
            canvas.y1 = 2;

            canvas.x2 = 62;
            canvas.y2 = 16;

            return canvas;
        }

        static void Main(string[] args)
        {
           
            Boolean dontShow = true;
            ConsoleKeyInfo consoleKeyPressed;

            Canvas canvas;
            canvas = GetCanvas();

            DrawDelegate drawingDelegates = null;
            IDrawManager drawManager = new DrawManager();
            DrawDotClass dotDrawClass = new DrawDotClass(drawManager);
            DrawCanvasClass drawCanvas = new DrawCanvasClass(drawManager);
            IFileParser jsonParser = new JsonFileParser();
            ISettingsProvider settingsProvider = new SettingsProvider(jsonParser);
            ISettings settings = SettingsProvider.GetSettings();

            DrawVerticalLineClass drawVerticalLineClass = new DrawVerticalLineClass(drawManager);
            DrawHorizontalLineClass drawHorizontalLineClass = new DrawHorizontalLineClass(drawManager);

            drawManager.DrawInitiate();
            drawManager.WriteAt(@"You can draw things by pressing key:
'1' - Draw canvas
'2' - Draw dot
'3' - Draw vertical line
'4' - Draw horizontal line

'q' - Errase canvas 
'w' - Errase dot
'e' - Errase vertical line
'r' - Errase horizontal line", 0, 0);
            do
            {
                consoleKeyPressed = Console.ReadKey(dontShow);

                switch (consoleKeyPressed.Key) //Switch on Key enum
                {
                    case ConsoleKey.D1: //Key "1"
                        drawingDelegates += new DrawDelegate(drawCanvas.DrawCanvas);
                        break;

                    case ConsoleKey.D2: //Key "2"
                            drawingDelegates += new DrawDelegate(dotDrawClass.DrawDot);
                            break;

                    case ConsoleKey.D3: //Key "3"
                        drawingDelegates += new DrawDelegate(drawVerticalLineClass.DrawVerticalLine);
                        break;

                    case ConsoleKey.D4: //Key "4"
                        drawingDelegates += new DrawDelegate(drawHorizontalLineClass.DrawHorizontallLine);
                        break;
                    case ConsoleKey.D6: //Key "4"
                        //drawingDelegates += DrawRandomDot;
                        break;


                    case ConsoleKey.Q: //Key "Q"
                        drawingDelegates -= new DrawDelegate(drawCanvas.DrawCanvas);
                        break;

                    case ConsoleKey.W: //Key "W"
                        drawingDelegates -= new DrawDelegate(dotDrawClass.DrawDot);
                        break;

                    case ConsoleKey.E: //Key "E"
                        drawingDelegates -= new DrawDelegate(drawVerticalLineClass.DrawVerticalLine);
                        break;

                    case ConsoleKey.R: //Key "R"
                        drawingDelegates -= new DrawDelegate(drawHorizontalLineClass.DrawHorizontallLine);
                        break;
                    case ConsoleKey.T: //Key "R"
                        //drawingDelegates -= DrawRandomDot;
                        break;
                    default:
                        break;
                }
                drawManager.ProceedDrawing(drawingDelegates, canvas);
            } while (consoleKeyPressed.Key != ConsoleKey.Escape);
        }

    }
}

