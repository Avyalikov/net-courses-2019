using DoorsAndLevelsGame.Interfaces;
using System;

namespace DoorsAndLevelsGame
{
    class Program
    {
        static void Main(string[] args)
        {
            IInputOutputDevice inputOutputDevice = new ConsoleInputOutputDevice();
            IDoorsNumbersGenerator doorsNumbersGenerator = new DoorsNumbersGenerator();
            ISettingsProvider settingsProvider = new SettingsProvider();
            IPhraseProvider phraseProvider = new PhraseProvider();
            GameSettings gameSettings = new GameSettings();

            Game game = new Game(doorsNumbersGenerator, inputOutputDevice, phraseProvider, settingsProvider, gameSettings);
            game.Run();
        }
    }
}
