namespace ConsoleCanvas
{
    using System;
    using System.Collections.Generic;
    using ConsoleCanvas.Interfaces;

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
            if (this.isInitialized)
            {
                return;
            }

            this.rawParsedData = this.fileParser.ParseFile(this.languageFilePath);
            foreach (string phraseKey in Enum.GetNames(typeof(Phrase)))
            {
                if (this.rawParsedData.ContainsKey(phraseKey))
                {
                    this.phrases[(Phrase)Enum.Parse(typeof(Phrase), phraseKey)] = this.rawParsedData[phraseKey];
                }
            }

            this.isInitialized = true;
        }

        public string GetPhrase(Phrase phrase)
        {
            this.Initialize();

            if (this.phrases.ContainsKey(phrase))
            {
                return this.phrases[phrase];
            }
            else
            {
                throw new Exception($"Phase provider can't find specific phrase: {phrase}");
            }
        }
    }
}