using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DoorsAndLevelsGame
{
    public class JSONSettingsProvider:ISettingsProvider 
    {

        public GameSettings GetGameSettings()
        {
            var resourceFile = new FileInfo("Resources\\GameSettings.json");

            if (!resourceFile.Exists)
            {
                throw new ArgumentException($"The settings file {resourceFile.Name} doesn't exist");
            }
            var resourceFileContent = File.ReadAllText(resourceFile.FullName);
            try
            {
                return JsonConvert.DeserializeObject<GameSettings>(resourceFileContent);

                
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't read the settings file", ex);
            }
        }
    }
}
