// <copyright file="ISettingsProvider.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace Trading.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Get Game Settings from source
    /// </summary>
    interface ISettingsProvider
    {
        /// <summary>
        /// Get settings from source
        /// </summary>
        /// <returns>Settings object with setting properties</returns>
        StockExchangeSettings GetSettings();
    }
}
