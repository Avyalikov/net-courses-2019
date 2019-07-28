using ConsoleCanvas.Interfaces;
using ConsoleCanvas;
using System;

namespace ConsoleCanvas
{
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
            ParseSettings();

            return new Settings(
                dotXOffsetPercent,
                dotYOffsetPercent,
                verticalLineXOffsetPercent,
                horizontalLineYOffsetPercent,
                canvasX1,
                canvasY1,
                canvasX2,
                canvasY2, 
                language);
        }

        private void ParseSettings()
        {
            var rawSettings = fileParser.ParseFile(settingsFilePath);

            //dotXOffsetPercent
            try
            {
                dotXOffsetPercent = Int32.Parse(rawSettings["dotXOffsetPercent"]);
            }
            catch
            {
                throw new Exception("Cant parse dotXOffsetPercent check settings file.");
            }

            //dotYOffsetPercent
            try
            {
                dotYOffsetPercent = Int32.Parse(rawSettings["dotYOffsetPercent"]);
            }
            catch
            {
                throw new Exception("Cant parse dotYOffsetPercent check settings file.");
            }

            //verticalLineXOffsetPercent
            try
            {
                verticalLineXOffsetPercent = Int32.Parse(rawSettings["verticalLineXOffsetPercent"]);
            }
            catch
            {
                throw new Exception("Cant parse verticalLineXOffsetPercent check settings file.");
            }

            //horizontalLineYOffsetPercent
            try
            {
                horizontalLineYOffsetPercent = Int32.Parse(rawSettings["horizontalLineYOffsetPercent"]);
            }
            catch
            {
                throw new Exception("Cant parse horizontalLineYOffsetPercent check settings file.");
            }

            //canvasX1
            try
            {
                canvasX1 = Int32.Parse(rawSettings["canvasX1"]);
            }
            catch
            {
                throw new Exception("Cant parse canvasX1 check settings file.");
            }

            //canvasY1
            try
            {
                canvasY1 = Int32.Parse(rawSettings["canvasY1"]);
            }
            catch
            {
                throw new Exception("Cant parse canvasY1 check settings file.");
            }

            //canvasX2
            try
            {
                canvasX2 = Int32.Parse(rawSettings["canvasX2"]);
            }
            catch
            {
                throw new Exception("Cant parse canvasX2 check settings file.");
            }

            //canvasY2
            try
            {
                canvasY2 = Int32.Parse(rawSettings["canvasY2"]);
            }
            catch
            {
                throw new Exception("Cant parse canvasY2 check settings file.");
            }

            //canvasY2
            try
            {
                language = rawSettings["language"];
            }
            catch
            {
                throw new Exception("Cant parse language check settings file.");
            }
        }
    }
}

