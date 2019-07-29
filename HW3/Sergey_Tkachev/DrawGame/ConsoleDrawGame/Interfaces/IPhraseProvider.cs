using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDrawGame.Interfaces
{
    interface IPhraseProvider
    {
        /// <summary>Get phase by the key.</summary>
        /// <param name="phraseKey">Key for phrase</param>
        /// <returns></returns>
        string GetPhrase(string phraseKey);
    }
}
