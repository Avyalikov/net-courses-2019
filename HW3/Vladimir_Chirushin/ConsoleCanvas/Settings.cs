namespace ConsoleCanvas
{
    using ConsoleCanvas.Interfaces;

    public class Settings : ISettings
    {
        public Settings(
               int dotXOffsetPercent,
               int dotYOffsetPercent,
               int verticalLineXOffsetPercent,
               int horizontalLineYOffsetPercent,
               int canvasX1,
               int canvasY1,
               int canvasX2,
               int canvasY2,
               string language)
        {
            this.DotXOffset = dotXOffsetPercent;
            this.DotYOffset = dotYOffsetPercent;
            this.VerticalLineXOffsetPercent = verticalLineXOffsetPercent;
            this.HorizontalLineYOffsetPercent = horizontalLineYOffsetPercent;
            this.Language = language;
            this.Board = new Board(canvasX1, canvasY1, canvasX2, canvasY2);
        }

        public int DotXOffset { get; private set; }

        public int DotYOffset { get; private set; }

        public int VerticalLineXOffsetPercent { get; private set; }

        public int HorizontalLineYOffsetPercent { get; private set; }

        public string Language { get; private set; }

        public IBoard Board { get; private set; }
    }
}