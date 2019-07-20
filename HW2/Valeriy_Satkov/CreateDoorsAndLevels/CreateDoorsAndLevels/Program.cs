using System;
using System.Collections.Generic;

namespace CreateDoorsAndLevels
{
    class Program
    {
        static void Main()
        {
            Interfaces.IPhraseProvider phraseProvider = new Modules.JsonPhraseProvider();

            new Game(phraseProvider: phraseProvider) { }.Run();

            Console.ReadKey();
        }        
    }
}
