namespace ConsoleCanvas
{
    using System;
    using ConsoleCanvas;
    using ConsoleCanvas.Interfaces;

    public class FileSettingsProvider : ISettingsProvider
    {
        private readonly string settingsFilePath;
        private readonly IDictionaryParser fileParser;
        private int dotXOffsetPercent;
        private int dotYOffsetPercent;
        private int verticalLineXOffsetPercent;
        private int horizontalLineYOffsetPercent;
        private int canvasX1;
        private int canvasY1;
        private int canvasX2;
        private int canvasY2;
        private string language;

        public FileSettingsProvider(IDictionaryParser parser, string settingsFilePath)
        {
            this.fileParser = parser;
            this.settingsFilePath = settingsFilePath;
        }

        public ISettings GetSettings()
        {
            this.ParseSettings();

            return new Settings(
                this.dotXOffsetPercent,
                this.dotYOffsetPercent,
                this.verticalLineXOffsetPercent,
                this.horizontalLineYOffsetPercent,
                this.canvasX1,
                this.canvasY1,
                this.canvasX2,
                this.canvasY2,
                this.language);
        }

        private void ParseSettings()
        {
            var rawSettings = this.fileParser.ParseFile(this.settingsFilePath);

            // dotXOffsetPercent
            try
            {
                this.dotXOffsetPercent = int.Parse(rawSettings["dotXOffsetPercent"]);
            }
            catch
            {
                throw new Exception("Cant parse dotXOffsetPercent check settings file.");
            }

            // dotYOffsetPercent
            try
            {
                this.dotYOffsetPercent = int.Parse(rawSettings["dotYOffsetPercent"]);
            }
            catch
            {
                throw new Exception("Cant parse dotYOffsetPercent check settings file.");
            }

            // verticalLineXOffsetPercent
            try
            {
                this.verticalLineXOffsetPercent = int.Parse(rawSettings["verticalLineXOffsetPercent"]);
            }
            catch
            {
                throw new Exception("Cant parse verticalLineXOffsetPercent check settings file.");
            }

            // horizontalLineYOffsetPercent
            try
            {
                this.horizontalLineYOffsetPercent = int.Parse(rawSettings["horizontalLineYOffsetPercent"]);
            }
            catch
            {
                throw new Exception("Cant parse horizontalLineYOffsetPercent check settings file.");
            }

            // canvasX1
            try
            {
                this.canvasX1 = int.Parse(rawSettings["canvasX1"]);
            }
            catch
            {
                throw new Exception("Cant parse canvasX1 check settings file.");
            }

            // canvasY1
            try
            {
                this.canvasY1 = int.Parse(rawSettings["canvasY1"]);
            }
            catch
            {
                throw new Exception("Cant parse canvasY1 check settings file.");
            }

            // canvasX2
            try
            {
                this.canvasX2 = int.Parse(rawSettings["canvasX2"]);
            }
            catch
            {
                throw new Exception("Cant parse canvasX2 check settings file.");
            }

            // canvasY2
            try
            {
                this.canvasY2 = int.Parse(rawSettings["canvasY2"]);
            }
            catch
            {
                throw new Exception("Cant parse canvasY2 check settings file.");
            }

            // canvasY2
            try
            {
                this.language = rawSettings["language"];
            }
            catch
            {
                throw new Exception("Cant parse language check settings file.");
            }
        }
    }
}