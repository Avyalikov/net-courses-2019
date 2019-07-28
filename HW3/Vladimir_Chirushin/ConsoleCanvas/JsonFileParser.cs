using ConsoleCanvas.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleCanvas
{
    public class JsonFileParser : IDictionaryParser
    {
        public Dictionary<string, string> ParseFile(string filePath)
        {
            FileInfo resourceFile = new FileInfo(filePath);
            if (!resourceFile.Exists)
            {
                throw new FileNotFoundException(
                    $"Can't find language file LangEN.json. Trying to find it here: {resourceFile}");
            }

            string resourceFileContent = File.ReadAllText(resourceFile.FullName);
            Dictionary<string, string> resourceData;
            try
            {
                resourceData = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Can't extract json value", e);
            }

            return resourceData;
        }
    }
}

