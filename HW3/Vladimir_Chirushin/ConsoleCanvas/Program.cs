using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCanvas
{
    public struct Canvas
    {
        public int x1;
        public int y1;

        public int x2;
        public int y2;
    }

    delegate void DrawDelegate(Canvas canvas);


    class DrawDotClass
    {
        public void DrawDot(Canvas canvas)
        {
            int dotXPos = (int)(canvas.x1 + (canvas.x2 - canvas.x1) * 0.3);
            int dotYPos = (int)(canvas.y1 + (canvas.y2 - canvas.y1) * 0.3);

            //WriteAt(".", dotXPos, dotYPos);
        }
    }
   

    class Program
    {
        protected static int origRow;
        protected static int origCol;

        public static void DrawCanvas(Canvas canvas)
        {
            for(int i = canvas.x1; i<canvas.x2; i++)    //drawing horizontal lines
            {
                WriteAt("-", i, canvas.y1);
                WriteAt("-", i, canvas.y2);
            }

            for (int i = canvas.y1; i < canvas.y2; i++)   //drawing vertical lines
            {
                WriteAt("|", canvas.x1, i);
                WriteAt("|", canvas.x2, i);
            }


            WriteAt("+", canvas.x1, canvas.y1);     //drawing fancy corners
            WriteAt("+", canvas.x1, canvas.y2);

            WriteAt("+", canvas.x2, canvas.y1);
            WriteAt("+", canvas.x2, canvas.y2);
        }


        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public static void DrawVerticalLine(Canvas canvas)
        {
            int lineXPos = (int)((canvas.x2 - canvas.x1)/2);
            for(int i = canvas.y1; i < canvas.y2; i++)
            {
                WriteAt("|", lineXPos, i);
            }
            WriteAt("+", lineXPos, canvas.y1);     //drawing fancy corners
            WriteAt("+", lineXPos, canvas.y2);
        }


        public static void DrawHorizontallLine(Canvas canvas)
        {

            int dotYPos = (int)((canvas.y2 - canvas.y1) / 2);
            for (int i = canvas.x1; i < canvas.x2; i++)
            {
                WriteAt("-", i, dotYPos);
            }
            WriteAt("+", canvas.x1, dotYPos);     //drawing fancy corners
            WriteAt("+", canvas.x2, dotYPos);
        }

        public static void ProceedDrawing(DrawDelegate drawDelegat, Canvas canvas)
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

        static void Main(string[] args)
        {
            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;
            Boolean dontShow = true;

            ConsoleKeyInfo consoleKeyPressed;

            Canvas canvas = new Canvas();
            canvas.x1 = 3;
            canvas.y1 = 2;

            canvas.x2 = 62;
            canvas.y2 = 16;

            DrawDelegate drawingDelegates = null;

            DrawDotClass dotDrawClass = new DrawDotClass();

            do
            {
                consoleKeyPressed = Console.ReadKey(dontShow);

                switch (consoleKeyPressed.Key) //Switch on Key enum
                {
                    case ConsoleKey.D1: //Key "1"
                        drawingDelegates += DrawCanvas;
                        break;

                    case ConsoleKey.D2: //Key "2"
                            drawingDelegates += new DrawDelegate(dotDrawClass.DrawDot);
                            break;

                    case ConsoleKey.D3: //Key "3"
                        drawingDelegates += DrawVerticalLine;
                        break;

                    case ConsoleKey.D4: //Key "4"
                        drawingDelegates += DrawHorizontallLine;
                        break;
                    case ConsoleKey.D6: //Key "4"
                        //drawingDelegates += DrawRandomDot;
                        break;


                    case ConsoleKey.Q: //Key "Q"
                        drawingDelegates -= DrawCanvas;
                        break;

                    case ConsoleKey.W: //Key "W"
                        //drawingDelegates -= DrawDot;
                        break;

                    case ConsoleKey.E: //Key "E"
                        drawingDelegates -= DrawVerticalLine;
                        break;

                    case ConsoleKey.R: //Key "R"
                        drawingDelegates -= DrawHorizontallLine;
                        break;
                    case ConsoleKey.T: //Key "R"
                        //drawingDelegates -= DrawRandomDot;
                        break;
                    default:
                        break;
                }
                ProceedDrawing(drawingDelegates, canvas);
            } while (consoleKeyPressed.Key != ConsoleKey.Escape);
        }
    }
}

