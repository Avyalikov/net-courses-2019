namespace ConsoleCanvas
{
    public class DrawVerticalLineClass : IDrawVerticalLineClass
    {
        private readonly IDrawManager drawManager;
        private int verticalLineXOffsetPercent;
        public DrawVerticalLineClass(IDrawManager drawManager, int verticalLineXOffsetPercent)
        {
            this.drawManager = drawManager;
            this.verticalLineXOffsetPercent = verticalLineXOffsetPercent;
        }
        public void DrawVerticalLine(Canvas canvas)
        {
            int lineXPos = (int)(canvas.x1 + ((canvas.x2 - canvas.x1) * verticalLineXOffsetPercent / 100));
            for (int i = canvas.y1; i < canvas.y2; i++)
            {
                drawManager.WriteAt("|", lineXPos, i);
            }
            drawManager.WriteAt("+", lineXPos, canvas.y1);     //drawing fancy ends
            drawManager.WriteAt("+", lineXPos, canvas.y2);
        }

    }
}

