using System;
using System.Collections.Generic;

namespace doors_levels
{
    public enum Phrase {welcome, weHaveDoors, weSelectNumber, andGoNextLevel, andGoPrevLevel, itsFirstLevel, youGetToFar, doorDoesntExist, pleaseEnterANumber}
    public class PhraseProvider : IPhraseProvider
    {
        Dictionary<Phrase, String> gamePhrases = new Dictionary<Phrase, String>();
        public PhraseProvider()
        {
            gamePhrases.Add(Phrase.welcome, "Welcome to amazing doors levels game!");
            gamePhrases.Add(Phrase.weHaveDoors, "We have numbers: ");
            gamePhrases.Add(Phrase.weSelectNumber, "We select number");
            gamePhrases.Add(Phrase.andGoNextLevel, " and go to next level: ");
            gamePhrases.Add(Phrase.andGoPrevLevel, "and go to previous level: ");
            gamePhrases.Add(Phrase.itsFirstLevel, "It's first level. Cant get higher.");
            gamePhrases.Add(Phrase.youGetToFar, "You get too far! Droped to first level");
            gamePhrases.Add(Phrase.doorDoesntExist, "Door doesn't exist");
            gamePhrases.Add(Phrase.pleaseEnterANumber, "Please enter a number.");
        }

        public String GetPhrase(Phrase requestedPhrase)
        {
            if (gamePhrases.ContainsKey(requestedPhrase))
            {
                return gamePhrases[requestedPhrase];
            }
            else
            {
                return "";
            }

        }
    }
}
