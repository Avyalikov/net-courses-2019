namespace ConsoleCanvas
{
    public class DrawDotClass : IDrawDotClass
    {
        private readonly IDrawManager drawManager;
        private int dotXOffsetPercent;
        private int dotYOffsetPercent;
        public DrawDotClass(IDrawManager drawManager, int dotXOffsetPercent, int dotYOffsetPercent)
        {
            this.drawManager = drawManager;
            this.dotXOffsetPercent = dotXOffsetPercent;
            this.dotYOffsetPercent = dotYOffsetPercent;
        }
        public void DrawDot(Canvas canvas)
        {
            int dotXPos = (int)(canvas.x1 + ((canvas.x2 - canvas.x1) * dotXOffsetPercent / 100));
            int dotYPos = (int)(canvas.y1 + ((canvas.y2 - canvas.y1) * dotYOffsetPercent / 100));

            drawManager.WriteAt(".", dotXPos, dotYPos);
        }
    }
}

