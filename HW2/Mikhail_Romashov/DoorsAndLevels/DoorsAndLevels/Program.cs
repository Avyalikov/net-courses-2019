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
            var lang = Languages.Eng;
            IInputOutputComponent inputOutputComponent = new ConsoleInputOutput();
            IDoorsNumbersGenerator doorsNumbersGenerator = new DoorsNumbersGenerator();
            ISettingsProvider settingsProvider = new SettingsProvider();
            IPhraseProvider phraseProvider = new PhraseProvider(lang);
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
