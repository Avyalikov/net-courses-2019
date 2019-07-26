using System;
using System.Collections.Generic;
using System.Linq;
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


    public class DrawDotClass
    {
        DrawManager drawManager;
        public DrawDotClass(DrawManager drawManager)
        {
            this.drawManager = drawManager;
        }
        public void DrawDot(Canvas canvas)
        {
            int dotXPos = (int)(canvas.x1 + (canvas.x2 - canvas.x1) * 0.3);
            int dotYPos = (int)(canvas.y1 + (canvas.y2 - canvas.y1) * 0.3);

            drawManager.WriteAt(".", dotXPos, dotYPos);
        }
    }

    public class DrawCanvasClass
    {
        DrawManager drawManager;
        public DrawCanvasClass(DrawManager drawManager)
        {
            this.drawManager = drawManager;
        }
        public void DrawCanvas(Canvas canvas)
        {
            for (int i = canvas.x1; i < canvas.x2; i++)    //drawing horizontal lines
            {
                drawManager.WriteAt("-", i, canvas.y1);
                drawManager.WriteAt("-", i, canvas.y2);
            }

            for (int i = canvas.y1; i < canvas.y2; i++)   //drawing vertical lines
            {
                drawManager.WriteAt("|", canvas.x1, i);
                drawManager.WriteAt("|", canvas.x2, i);
            }


            drawManager.WriteAt("+", canvas.x1, canvas.y1);     //drawing fancy corners
            drawManager.WriteAt("+", canvas.x1, canvas.y2);

            drawManager.WriteAt("+", canvas.x2, canvas.y1);
            drawManager.WriteAt("+", canvas.x2, canvas.y2);
        }
    }

    public class DrawVerticalLineClass
    {
        DrawManager drawManager;
        public DrawVerticalLineClass(DrawManager drawManager)
        {
            this.drawManager = drawManager;
        }
        public void DrawVerticalLine(Canvas canvas)
        {
            int lineXPos = (int)((canvas.x2 - canvas.x1) / 2);
            for (int i = canvas.y1; i < canvas.y2; i++)
            {
                drawManager.WriteAt("|", lineXPos, i);
            }
            drawManager.WriteAt("+", lineXPos, canvas.y1);     //drawing fancy ends
            drawManager.WriteAt("+", lineXPos, canvas.y2);
        }

    }

    public class DrawHorizontalLineClass
    {
        DrawManager drawManager;
        public DrawHorizontalLineClass(DrawManager drawManager)
        {
            this.drawManager = drawManager;
        }
        public void DrawHorizontallLine(Canvas canvas)
        {

            int dotYPos = (int)((canvas.y2 - canvas.y1) / 2);
            for (int i = canvas.x1; i < canvas.x2; i++)
            {
                drawManager.WriteAt("-", i, dotYPos);
            }
            drawManager.WriteAt("+", canvas.x1, dotYPos);     //drawing fancy ends
            drawManager.WriteAt("+", canvas.x2, dotYPos);
        }

    }

    public class DrawManager
    {
        protected static int origRow;
        protected static int origCol;
        public void DrawInitiate()
        {
            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;
        }
        public void WriteAt(string userString, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(userString);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }


        public void ProceedDrawing(DrawDelegate drawDelegat, Canvas canvas)
        {
            Console.Clear();
            if (drawDelegat != null)
            {
                drawDelegat(canvas);
                WriteAt($"There is {drawDelegat.GetInvocationList().Count().ToString()} objects on canvas!", 0, 28);
            }
            else
            {
                WriteAt($"Canvas is clean!", 0, 28);
            }
        }
    }





    class Program
    {

        private static Canvas GetCanvas()
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
            DrawManager drawManager = new DrawManager();
            DrawDotClass dotDrawClass = new DrawDotClass(drawManager);
            DrawCanvasClass drawCanvas = new DrawCanvasClass(drawManager);
            DrawVerticalLineClass drawVerticalLineClass = new DrawVerticalLineClass(drawManager);
            DrawHorizontalLineClass drawHorizontalLineClass = new DrawHorizontalLineClass(drawManager);


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

