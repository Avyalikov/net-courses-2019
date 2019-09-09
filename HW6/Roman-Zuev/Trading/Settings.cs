// <copyright file="GameSettings.cs" company=".net courses 2019">
// All rights reserved.
// </copyright>
// <author>Roman Zuev</author>

namespace Trading.DataModel
{
    /// <summary>
    /// Defines the <see cref="GameSettings" />
    /// </summary>
    internal class Settings
    {
        internal string Language { get; set; }
        internal char ExitCode { get; set; }

        internal double TransactionsTimeout { get; set; }
        internal char PauseTrades { get; set; }
        internal int MaxSharesToSell { get; set; }
    }
}
