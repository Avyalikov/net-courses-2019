namespace DoorsAndLevelsRefactoring.Provider
{
    using DoorsAndLevelsRefactoring.Interface;
    using Newtonsoft.Json;
    using System;
    using System.IO;

    class SettingProvider : ISettingProvider
    {
        public GameSettings GetGameSettings()
        {
            var gameSettingFile = new FileInfo("gameSetting.json");

            if (!gameSettingFile.Exists)
            {
                throw new ArgumentException(
                    string.Format("Can't find game settings file {0}", 
                    gameSettingFile.FullName));
            }

            var textContent = File.ReadAllText(gameSettingFile.FullName);

            try
            {
                return JsonConvert.DeserializeObject<GameSettings>(textContent);
            }
            catch(Exception e)
            {
                throw new ArgumentException(
                    "Can't read game settings content ", e.Message);
            }
        }
    }
}
