namespace ConsoleCanvas
{
    public class Settings :ISettings
    {
        Canvas canvas;
        private int dotXOffsetPercent;
        private int dotYOffsetPercent;

        private int verticalLineXOffsetPercent;
        private int horizontalLineYOffsetPercent;

        private string language;

        public Settings(
            int dotXOffsetPercent,
            int dotYOffsetPercent,
            int verticalLineXOffsetPercent,
            int horizontalLineYOffsetPercent,
            int canvasX1,
            int canvasY1,
            int canvasX2,
            int canvasY2,
            string language
            )
        {
            this.dotXOffsetPercent = dotXOffsetPercent;
            this.dotYOffsetPercent = dotYOffsetPercent;           
            this.verticalLineXOffsetPercent = verticalLineXOffsetPercent;
            this.horizontalLineYOffsetPercent = horizontalLineYOffsetPercent;
            this.language = language;

            this.canvas = new Canvas(canvasX1, canvasY1, canvasX2, canvasY2);

        }

        public int GetDotXOffset() { return dotXOffsetPercent; }
        public int GetDotYOffset() { return dotYOffsetPercent; }
        public int GetVerticalLineXOffset() { return verticalLineXOffsetPercent; }
        public int GetHorizontalLineYOffset() { return horizontalLineYOffsetPercent; }
        public Canvas GetCanvas() { return canvas; }
        public string GetLanguage() { return language; }

    }
}

