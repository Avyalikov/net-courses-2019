using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doors_and_levels_game_after_refactoring
{
    public interface IPhraseProvider
    {
        string GetPhrase(string phraseKey);
    }
}
