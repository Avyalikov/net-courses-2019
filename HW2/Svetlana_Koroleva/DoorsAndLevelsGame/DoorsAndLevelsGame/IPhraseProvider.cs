using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevelsGame
{
    public interface IPhraseProvider
    {
        string GetPhrase(string keyword);
        void ReadResourceFile();
    }
}
