using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSimulator.ConsoleApp.Interfaces
{
    internal interface IPhraseProvider
    {
        string GetPhrase(string langPackName, KeysForPhrases phraseKey);
    }
}
