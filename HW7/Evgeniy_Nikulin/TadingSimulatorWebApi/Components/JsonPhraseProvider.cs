namespace TradingSimulatorWebApi.Components
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;
    using TradingSimulator.Core;
    using TradingSimulator.Core.Interfaces;

    public class JsonPhraseProvider : IPhraseProvider
    {

        private readonly GameSettings gs;
        private bool InitFlag = false;

        public JsonPhraseProvider(GameSettings gs)
        {
            this.gs = gs;            
        }

        private Dictionary<string, string> resourceData = new Dictionary<string, string>();

        public void Init()
        {
            var resourceFile = new FileInfo($"Resources/{gs.Language}Language.json");
            if (!resourceFile.Exists)
            {
                throw new ArgumentException(
                    $"Can't find language json file. Trying to find it here: {resourceFile.FullName}");
            }

            var resourceFileContent = File.ReadAllText(resourceFile.FullName);
            try
            {
                this.resourceData = JsonConvert.DeserializeObject<Dictionary<string, string>>(resourceFileContent);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Can't read language file content", ex);
            }
        }

        public string GetPhrase(Phrase phrase)
        {
            if (!InitFlag)
            {
                Init();
                InitFlag = true;
            }

            try
            {
                return this.resourceData[phrase.ToString()];
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}