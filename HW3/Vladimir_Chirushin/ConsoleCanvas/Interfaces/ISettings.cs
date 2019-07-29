namespace ConsoleCanvas.Interfaces
{
    public interface ISettings
    {
        int DotXOffset { get; }

        int DotYOffset { get; }

        int VerticalLineXOffsetPercent { get; }

        int HorizontalLineYOffsetPercent { get; }

        string Language { get; }

        IBoard Board { get; }
    }
}