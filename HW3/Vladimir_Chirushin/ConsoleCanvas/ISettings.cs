namespace ConsoleCanvas
{
    public interface ISettings
    {
        int GetDotXOffset();
        int GetDotYOffset();
        int GetVerticalLineXOffset();
        int GetHorizontalLineYOffset();
        Canvas GetCanvas();
        string GetLanguage();
    }
}

