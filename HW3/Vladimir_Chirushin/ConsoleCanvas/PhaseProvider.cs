using System;
using System.Collections.Generic;

namespace ConsoleCanvas
{
    public enum Phrase
    {
        welcome, canvasDrawMessage, dotDrawMessage, horizontalDrawMessage, verticalDrawMessage, gooseDrawMessage,
        canvasErraseMesage, dotErraseMessage, horizontalErraseMessage, verticalErraseMessage, gooseErraseMessage
    }
    public class PhraseProvider : IPhraseProvider
    {
        private Dictionary<Phrase, String> gamePhrases = new Dictionary<Phrase, String>();
        private Dictionary<String, String> rawParsedData;
        private String languageFilePath;
        private readonly IFileParser fileParser;

        public PhraseProvider(IFileParser fileParser, string languageFilePath)
        {
            this.fileParser = fileParser;
            this.languageFilePath = languageFilePath;
        }


        public void InitiatePhrases()
        {
            rawParsedData = fileParser.ParseFile(languageFilePath);
            foreach (String phraseKey in Enum.GetNames(typeof(Phrase)))
            {
                if (rawParsedData.ContainsKey(phraseKey))
                {
                    gamePhrases.Add(
                        (Phrase)Enum.Parse(typeof(Phrase), phraseKey), rawParsedData[phraseKey]
                        );
                }
            }
        }
        public String GetPhrase(Phrase requestedPhrase)
        {
            if (gamePhrases.ContainsKey(requestedPhrase))
            {
                return gamePhrases[requestedPhrase];
            }
            else
            {
                throw new Exception($"Phase provider can't find specific phrase: {requestedPhrase}");
            }

        }
    }
}

