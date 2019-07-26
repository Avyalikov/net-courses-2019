namespace ConsoleCanvas
{
    public class DrawCanvasClass
    {
        IDrawManager drawManager;
        public DrawCanvasClass(IDrawManager drawManager)
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
}

