using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace doors_levels
{
    public class JsonFileParser : IFileParser
    {

        public Dictionary<String, String> ParseFile(String filePath)
        {
            FileInfo resourceFile = new FileInfo(filePath);
            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find language file LangEN.json. Trying to find it here: {resourceFile}");
            }

            String resourceFileContent = File.ReadAllText(resourceFile.FullName);
            Dictionary<String, String> resourceData;
            try
            {
                resourceData = JsonConvert.DeserializeObject<Dictionary<String, String>>(resourceFileContent);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't extract json value", ex);
            }

            return resourceData;
        }
    }
}
