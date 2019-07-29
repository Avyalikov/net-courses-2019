namespace Console_Draw_Game
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    class PhraseProvider : Interfaces.IPhraseProvider
    {
        private Dictionary<string, string> phrasesDict;
        private Dictionary<string, string> languages = new Dictionary<string, string>()
        {
            { "Rus", "..\\..\\..\\Res\\rusText.xml" },
            {  "Eng", "..\\..\\..\\Res\\engText.xml" }
        };

        public void ParseXML(string requireLang)
        {
            phrasesDict = new Dictionary<string, string>();

            XmlDocument textFile = new XmlDocument();

            var resFile = new FileInfo(languages[requireLang]);
            if (!resFile.Exists)
            {
                throw new ArgumentException(
                    $"Requirement file doesn't exists.");
            }

            textFile.Load(resFile.FullName);
            XmlElement root = textFile.DocumentElement;

            //add all phrases to dictionary
            foreach (XmlElement child in root)
            {
                phrasesDict.Add(child.Name, child.InnerText);
            }
        }

        public string GetMessage(string key)
        {
            try
            {
                return phrasesDict[key];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception($"The string with the key = '{key}' is not contained in the text file.");
            }
        }
    }
}
