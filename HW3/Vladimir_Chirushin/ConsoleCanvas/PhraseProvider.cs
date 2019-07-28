using ConsoleCanvas.Interfaces;
using System;
using System.Collections.Generic;

namespace ConsoleCanvas
{
    public enum Phrase
    {
        Welcome, CanvasDrawMessage, DotDrawMessage, HorizontalDrawMessage, VerticalDrawMessage, GooseDrawMessage,
        CanvasEraseMesage, DotEraseMessage, HorizontalEraseMessage, VerticalEraseMessage, GooseEraseMessage
    }

    public class PhraseProvider : IPhraseProvider
    {
        private readonly Dictionary<Phrase, string> phrases;
        private readonly string languageFilePath;
        private readonly IDictionaryParser fileParser;
        private Dictionary<string, string> rawParsedData;
        private bool isInitialized = false;

        public PhraseProvider(IDictionaryParser fileParser, string languageFilePath)
        {
            this.phrases = new Dictionary<Phrase, string>();
            this.fileParser = fileParser;
            this.languageFilePath = languageFilePath;
        }

        public void Initialize()
        {
            if (isInitialized)
            {
                return;
            }

            rawParsedData = fileParser.ParseFile(languageFilePath);
            foreach (string phraseKey in Enum.GetNames(typeof(Phrase)))
            {
                if (rawParsedData.ContainsKey(phraseKey))
                {
                    phrases[(Phrase)Enum.Parse(typeof(Phrase), phraseKey)] = rawParsedData[phraseKey];
                }
            }

            isInitialized = true;
        }

        public string GetPhrase(Phrase phrase)
        {
            Initialize();

            if (phrases.ContainsKey(phrase))
            {
                return phrases[phrase];
            }
            else
            {
                throw new Exception($"Phase provider can't find specific phrase: {phrase}");
            }
        }
    }
}

