using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevels
{
    class Program
    {
        static void Main(string[] args)
        {
            IInputOutputComponent inputOutputComponent = new ConsoleInputOutput();
            IDoorsNumbersGenerator doorsNumbersGenerator = new DoorsNumbersGenerator();
            ISettingsProvider settingsProvider = new SettingsProvider();
            IPhraseProvider phraseProvider = new PhraseProvider();
            IStorageComponent stackStorageComponent = new StackStorageComponent();

            DoorsAndLevels game = new DoorsAndLevels(
                inputOutputComponent, 
                doorsNumbersGenerator, 
                settingsProvider, 
                phraseProvider, 
                stackStorageComponent
            );
            game.Run();
        }
    }
}
