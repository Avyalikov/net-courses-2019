namespace ConsoleCanvas
{
    public class DrawHorizontalLineClass : IDrawHorizontalLineClass
    {
        private readonly IDrawManager drawManager;
        private int horizontalLineYOffsetPercent;
        public DrawHorizontalLineClass(IDrawManager drawManager, int horizontalLineYOffsetPercent)
        {
            this.drawManager = drawManager;
            this.horizontalLineYOffsetPercent = horizontalLineYOffsetPercent;
        }
        public void DrawHorizontallLine(Canvas canvas)
        {

            int lineYPos = (int)(canvas.y1 + ((canvas.y2 - canvas.y1) * horizontalLineYOffsetPercent / 100));
            for (int i = canvas.x1; i < canvas.x2; i++)
            {
                drawManager.WriteAt("-", i, lineYPos);
            }
            drawManager.WriteAt("+", canvas.x1, lineYPos);     //drawing fancy ends
            drawManager.WriteAt("+", canvas.x2, lineYPos);
        }

    }
}

