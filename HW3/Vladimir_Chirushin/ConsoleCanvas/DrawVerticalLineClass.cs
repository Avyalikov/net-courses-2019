namespace ConsoleCanvas
{
    public class DrawVerticalLineClass
    {
        IDrawManager drawManager;
        public DrawVerticalLineClass(IDrawManager drawManager)
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
}

