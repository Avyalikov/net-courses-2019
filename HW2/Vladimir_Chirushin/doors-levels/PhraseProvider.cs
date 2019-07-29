using System;
using System.Collections.Generic;

namespace doors_levels
{
    public enum Phrase {welcome, weHaveDoors, weSelectNumber, andGoNextLevel, andGoPrevLevel, itsFirstLevel, youGetToFar, doorDoesntExist, pleaseEnterANumber}
    public class PhraseProvider : IPhraseProvider
    {
        Dictionary<Phrase, String> gamePhrases = new Dictionary<Phrase, String>();
        Dictionary<String,String> rawParsedData;
        String languageFilePath;
        IFileParser fileParser;
        
        public PhraseProvider(IFileParser fileParser, String languageFilePath)
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
