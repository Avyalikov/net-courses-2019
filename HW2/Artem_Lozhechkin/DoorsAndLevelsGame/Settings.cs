using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevelsGame
{
    /// <summary>
    /// This class loads and contains all the Game settings.
    /// </summary>
    class Settings
    {
        public readonly Languages language;
        public readonly Dictionary<PhraseTypes, string> phrases;
        public readonly int numberOfDoors;
        public Settings(Languages language, Dictionary<PhraseTypes, string> phrases, int numberOfDoors)
        {
            this.numberOfDoors = numberOfDoors;
            this.language = language;
            this.phrases = phrases;
        }
    }
}
