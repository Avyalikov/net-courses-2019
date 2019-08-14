using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Interfaces
{
    interface IPhraseProvider
    {
        string GetPhrase(string phraseKey);
    }
}
