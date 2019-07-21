using System;

namespace doors_levels
{
    public interface IPhraseProvider
    {
        String GetPhrase(Phrase requestedPhrase);
    }

}
