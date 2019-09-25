using System;
using System.Collections.Generic;

namespace doors_levels
{
    public class GameSettings : IGameSettings
    {
        IFileParser fileParser;
        String pathToSettingsFile;

        private Int32 MaxDoors;
        private Int32 minDoorValue;
        private Int32 maxDoorValue;
        private String gameExitCommand;

        private String languageFilePath;

        Dictionary<String, String> rawParsedData = new Dictionary<String, String>();
        public GameSettings (IFileParser fileParser, String pathToSettingsFile)
        {
            this.fileParser = fileParser;
            this.pathToSettingsFile = pathToSettingsFile;
        }

        public void InitiateSettings()
        {
            rawParsedData = fileParser.ParseFile(pathToSettingsFile);

            try
            {
                MaxDoors = Convert.ToInt32(rawParsedData["maxDoors"]);
            }
            catch
            {
                throw new Exception("Errors in settings.json");
            }

            languageFilePath = rawParsedData["languageFile"];

            try
            {
                maxDoorValue = Convert.ToInt32(rawParsedData["maxDoorValue"]);
            }
            catch
            {
                throw new Exception("Errors in settings.json");
            }

            try
            {
                minDoorValue = Convert.ToInt32(rawParsedData["minDoorValue"]);
            }
            catch
            {
                throw new Exception("Errors in settings.json");
            }

            gameExitCommand = rawParsedData["gameExitCommand"];
        }


        public Int32 GetMaxDoors()
        {
            return MaxDoors;
        }

        public Int32 GetMaxDoorValue()
        {
            return maxDoorValue;
        }

        public Int32 GetMinDoorValue()
        {
            return minDoorValue;
        }

        public String GetExitCommand()
        {
            return gameExitCommand;
        }


        public String GetLanguagePath()
        {
            return languageFilePath;
        }
    }
}
