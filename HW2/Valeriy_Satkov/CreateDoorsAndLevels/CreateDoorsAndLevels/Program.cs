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
            Interfaces.IDoorsNumbersGenerator doorsNumbersGenerator = new Modules.DoorsNumbersGenerator();

            new Game(
                phraseProvider: phraseProvider, 
                inputOutputDevice: inputOutputDevice,
                doorsNumbersGenerator: doorsNumbersGenerator
                ) { }.Run();

            inputOutputDevice.ReadKey(); // pause
        }        
    }
}
