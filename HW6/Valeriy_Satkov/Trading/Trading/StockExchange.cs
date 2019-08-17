// <copyright file="StockExchange.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace Trading
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using System.Linq;
    using Entities;
    using System.Text;

    /// <summary>
    /// Basic game logic class
    /// </summary>
    class StockExchange
    {
        /// <summary>
        /// Provides settings from a file
        /// </summary>
        private readonly ISettingsProvider settingsProvider;

        /// <summary>
        /// Provides Input-Output device, Console
        /// </summary>
        private readonly IInputOutputDevice ioProvider;

        /// <summary>
        /// Provides descriptions(phrases) from a file
        /// </summary>
        private readonly IPhraseProvider phraseProvider;

        /// <summary>
        /// Defines game settings
        /// </summary>
        private readonly StockExchangeSettings settings;

        /// <summary>
        /// Defines database context
        /// </summary>
        private readonly IContext db;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockExchange"/> class.
        /// </summary>
        /// <param name="settingsProvider">The settingsProvider<see cref="ISettingsProvider"/></param>
        /// <param name="ioProvider">The inputOutputDevice<see cref="IInputOutputDevice"/></param>
        /// <param name="phraseProvider">The phraseProvider<see cref="IPhraseProvider"/></param>
        /// <param name="context">The board<see cref="IContext"/></param>
        public StockExchange(
            ISettingsProvider settingsProvider, 
            IInputOutputDevice ioProvider,
            IPhraseProvider phraseProvider, 
            IContext context)
        {
            this.settingsProvider = settingsProvider;
            this.ioProvider = ioProvider;
            this.phraseProvider = phraseProvider;

            this.settings = this.settingsProvider.GetSettings();

            this.db = context;
        }

        /// <summary>
        /// Logic start method
        /// </summary>
        public void Start()
        {
            this.phraseProvider.SetLanguage(this.settings.Language);

            string s;

            do
            {
                this.ioProvider.Clear();

                /* Menu
                 * 1. Start traiding
                 * 2. Clients in 'Orange' zone
                 * 3. Change the cost of share type
                 * 4. Add a new share into system
                 * 5. Add a new share type into system
                 * 6. Add a new client
                 */
                this.ioProvider.WriteLine("Menu");
                this.ioProvider.WriteLine($" 6. {this.phraseProvider.GetPhrase("AddClient")}");
                this.ioProvider.WriteLine(String.Empty);
                this.ioProvider.WriteLine(this.phraseProvider.GetPhraseAndReplace("Enter", "ExitCode", settings.ExitCode));

                s = ioProvider.ReadLine();
                switch (s)
                {
                    case "6":
                        this.ioProvider.WriteLine("You have chosen 6"); // signal about enter into case
                        AddClient();
                        // ioProvider.ReadLine(); // pause
                        break;
                    default:
                        break;
                }
            } while (s != settings.ExitCode);
        }

        public void AddClient()
        {
            string lastName = string.Empty,
                firstName = string.Empty,
                phone = string.Empty;
            decimal balance = 0;                      

            string outputString = string.Empty;
            while (outputString != settings.ExitCode)
            {
                if (string.IsNullOrEmpty(lastName))
                {
                    this.ioProvider.Write(this.phraseProvider.GetPhrase("AddClient_LastName"));
                    outputString = ioProvider.ReadLine();
                    if (outputString.Length < 2 || outputString.Length > 20)
                    {
                        ioProvider.WriteLine(this.phraseProvider.GetPhrase("WrongLine"));
                        continue;
                    }
                    lastName = outputString;
                }

                if (string.IsNullOrEmpty(firstName))
                {
                    this.ioProvider.Write(this.phraseProvider.GetPhrase("AddClient_FirstName"));
                    outputString = ioProvider.ReadLine();
                    if (outputString.Length < 2 || outputString.Length > 20)
                    {
                        ioProvider.WriteLine(this.phraseProvider.GetPhrase("WrongLine"));
                        continue;
                    }
                    firstName = outputString;
                }

                if (string.IsNullOrEmpty(phone))
                {
                    this.ioProvider.Write(this.phraseProvider.GetPhrase("AddClient_Phone"));
                    outputString = ioProvider.ReadLine();
                    if (outputString.Length < 2 || outputString.Length > 20)
                    {
                        ioProvider.WriteLine(this.phraseProvider.GetPhrase("WrongLine"));
                        continue;
                    }
                    phone = outputString;
                }

                if (balance <= 0)
                {
                    this.ioProvider.Write(this.phraseProvider.GetPhrase("AddClient_Balance"));
                    bool flag = decimal.TryParse(outputString = ioProvider.ReadLine(), out balance);
                    if (!flag || balance <= 0)
                    {
                        ioProvider.WriteLine(this.phraseProvider.GetPhrase("WrongLine"));
                        continue;
                    }
                }

                // context stored procedure add client {lastName, firstName, phone, balance}
                db.Clients.Add(new Client()
                {
                    LastName = lastName,
                    FirstName = firstName,
                    Phone = phone,
                    Balance = balance
                });

                db.SaveChanges();

                ioProvider.WriteLine("New client was added!");
                ioProvider.ReadLine(); // pause

                break;
            }
        }
    }
}
