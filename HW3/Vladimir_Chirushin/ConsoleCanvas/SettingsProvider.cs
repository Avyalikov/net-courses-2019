namespace ConsoleCanvas
{
    public class SettingsProvider
    {
        private IFileParser fileParser
        public SettingsProvider(IFileParser fileParser)
        {
            this.fileParser = fileParser;
        }

    }

    public GetSettings()
    {
        
        return new Settings(dotXOffsetPercent,dotYOffsetPercent,verticalLineXOffsetPercent, horizontalLineYOffsetPercent);
    }
}

