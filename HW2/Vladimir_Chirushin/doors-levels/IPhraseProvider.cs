using System;

namespace doors_levels
{
    public interface IPhraseProvider
    {
        void InitiatePhrases();
        String GetPhrase(Phrase requestedPhrase);
    }

}
