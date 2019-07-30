namespace Console_Draw_Game
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.IO;

    public class SettingsProvider : Interfaces.ISettingsProvider
    {
        private Dictionary<string, string> gameSettings;

        private string path = "..\\..\\..\\Res\\settings.xml";
        public void ParseXML()
        {
            gameSettings = new Dictionary<string, string>();

            XmlDocument textFile = new XmlDocument();

            var resFile = new FileInfo(path);

            if (!resFile.Exists)
            {
                throw new ArgumentException(
                    $"Settings file doesn't exists.");
            }

            textFile.Load(resFile.FullName);
            XmlElement root = textFile.DocumentElement;

            //add all settings to dictionary
            foreach (XmlElement child in root)
            {
                gameSettings.Add(child.Name, child.InnerText);
            }
        }

        public string GetSetting(string xmlKey)
        {
            try
            {
                return gameSettings[xmlKey];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception($"The string with the key = '{xmlKey}' doesn't exists in settings file.");
            }
        }
    }
}
