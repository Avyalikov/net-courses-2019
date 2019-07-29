using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevelsGame.Interfaces
{
    interface IPhraseProvider
    {
        string GetPhrase(string phraseKey);
    }
}
