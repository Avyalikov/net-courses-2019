namespace ConsoleCanvas
{
    public class Settings :ISettings
    {
        Canvas canvas;
        int dotXOffsetPercent;
        int dotYOffsetPercent;

        int verticalLineXOffsetPercent;
        int horizontalLineYOffsetPercent;

        public Settings(int dotXOffsetPercent, int dotYOffsetPercent, int verticalLineXOffsetPercent, int horizontalLineYOffsetPercent)
        {
            this.dotXOffsetPercent = dotXOffsetPercent;
            this.dotYOffsetPercent = dotYOffsetPercent;
            this.verticalLineXOffsetPercent = verticalLineXOffsetPercent;
            this.horizontalLineYOffsetPercent = horizontalLineYOffsetPercent;

        }

        public int GetDotXOffset() { return dotXOffsetPercent; }
        public int GetDotYOffset() { return dotYOffsetPercent; }
        public int GetVerticalLineXOffset() { return verticalLineXOffsetPercent; }
        public int GetHorizontalLineYOffset() { return GetHorizontalLineYOffset; }

    }
}

