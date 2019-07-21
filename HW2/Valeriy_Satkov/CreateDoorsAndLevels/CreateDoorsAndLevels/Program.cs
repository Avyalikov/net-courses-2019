using System;
using System.Collections.Generic;

namespace CreateDoorsAndLevels
{
    class Program
    {
        static void Main()
        {
            Interfaces.IPhraseProvider phraseProvider = new Modules.JsonPhraseProvider();
            Interfaces.IInputOutputDevice inputOutputDevice = new Modules.ConsoleInputOutputDevice();

            new Game(
                phraseProvider: phraseProvider, 
                inputOutputDevice: inputOutputDevice
                ) { }.Run();

            inputOutputDevice.ReadKey(); // pause
        }        
    }
}
