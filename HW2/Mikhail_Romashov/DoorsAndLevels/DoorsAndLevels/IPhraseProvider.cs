using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevels
{
    public interface IPhraseProvider
    {
        string GetPhrase(string phraseKey);
    }

}
