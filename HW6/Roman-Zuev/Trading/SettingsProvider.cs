using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.DataModel
{
    internal class SettingsProvider: ISettingsProvider
    {
        public Settings GetSettings()
        {
            Settings settings = new Settings {
                ExitCode = 'x',
                Language = "Eng",
                PauseTrades = ' ',
                TransactionsTimeout = 20000,
                MaxSharesToSell = 15 };
            return settings;
        }
    }
}
