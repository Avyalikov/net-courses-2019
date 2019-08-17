// <copyright file="Program.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace Trading
{
    using Components;
    using Interfaces;

    /// <summary>
    /// class with the entry point
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        static void Main(string[] args)
        {
            ISettingsProvider settingsProvider = new JsonSettingsProvider();
            IInputOutputDevice ioProvider = new ConsoleInputOutputDevice();
            IPhraseProvider phraseProvider = new JsonPhraseProvider();

            using (var context = new StockExchangeContext())
            {
                ioProvider.WriteLine("Please wait until db is loading...");
                context.Database.Initialize(false);
                ioProvider.WriteLine("The DB was loaded.");

                new StockExchange(
                    settingsProvider: settingsProvider,
                    ioProvider: ioProvider,
                    phraseProvider: phraseProvider,
                    context: context
                    ).Start();
            }            
        }
    }
}
