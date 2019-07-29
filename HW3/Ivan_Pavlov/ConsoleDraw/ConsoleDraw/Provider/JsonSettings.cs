namespace ConsoleDraw.Provider
{
    using Interfaces;
    using Newtonsoft.Json;
    using System;
    using System.IO;

    class JsonSettings : ISettingsProvider
    {
        public GameSettings GetGameSettings()
        {
            var gameSetting = new FileInfo("Resources\\GameSettings.json");

            if (!gameSetting.Exists)
            {
                throw new ArgumentException(string.Format(
                    "Can't find gamesettings json file. Trying to find it here {0}", 
                    gameSetting.FullName));
            }

            var texttContent = File.ReadAllText(gameSetting.FullName);

            try
            {
                return JsonConvert.DeserializeObject<GameSettings>(texttContent);
            }
            catch (Exception e)
            {
                throw new ArgumentException(string.Format(
                    "Can't read GameSetting content. {0}"), e.Message);
            }
        }
    }
}
