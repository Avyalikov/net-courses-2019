using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace DoorsAndLevels
{
    class SettingsProvider : ISettingsProvider
    {
        public GameSettings gameSettings()
        {
            GameSettings settings = new GameSettings();
            XmlDocument xmlDoc = new XmlDocument();

            var resourceFile = new FileInfo("Resources/settings.xml");

            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find file settings.xml. Trying to find it here: {resourceFile.FullName}");
            }

            xmlDoc.Load(resourceFile.FullName);
            XmlElement xmlRoot = xmlDoc.DocumentElement;

            foreach (XmlElement childNode in xmlRoot)
            {  
                if (childNode.Name == "doorsAmount")
                    settings.doorsAmount = Int32.Parse(childNode.InnerText);
                if (childNode.Name == "exitCode")
                    settings.exitCode = Int32.Parse(childNode.InnerText);
                if (childNode.Name == "previousLevelCode")
                    settings.previousLevelCode = Int32.Parse(childNode.InnerText);
                if (childNode.Name == "language")
                    settings.gameLanguage = childNode.InnerText;
            }

            return settings;
        }
    }
}
