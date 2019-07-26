namespace ConsoleCanvas
{
    public class DrawDotClass
    {
        IDrawManager drawManager;
        public DrawDotClass(IDrawManager drawManager)
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
}

