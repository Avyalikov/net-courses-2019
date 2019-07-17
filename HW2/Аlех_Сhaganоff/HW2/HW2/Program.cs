using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyType = System.Int32;

namespace HW2
{
    class Program
    {
        static void Main(string[] args)
        {
            IStorageProvider storageProvider = new StackStorage();
            IReadInputProvider readInputProvider = new ConsoleInput();
            ISendOutputProvider sendOutputProvider = new ConsoleOutput();
            IRandomProvider randomProvider = new Rand();
            ITextMessagesProvider textMessagesProvider = new XMLText();
            ISettingsProvider settingsProvider = new XMLSettings();

            Game game = new Game
            (
             storageProvider: storageProvider,
             readInputProvider: readInputProvider,
             sendOutputProvider: sendOutputProvider,
             randomProvider: randomProvider,
             textMessagesProvider: textMessagesProvider,
             settingsProvider: settingsProvider
            );

            game.run();
        }
    }
}
