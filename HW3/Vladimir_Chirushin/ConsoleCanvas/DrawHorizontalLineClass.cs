namespace ConsoleCanvas
{
    public class DrawHorizontalLineClass
    {
        IDrawManager drawManager;
        public DrawHorizontalLineClass(IDrawManager drawManager)
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
}

