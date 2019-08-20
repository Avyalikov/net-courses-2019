// <copyright file="IPriceModifier.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Model;

    /// <summary>
    /// IPriceModifier description
    /// </summary>
    public interface IPriceModifier
    {
        decimal GetTransactionPrice(TransactionHistory transaction);
        decimal GetPriceByDateTime(DateTime dateTime, int stockId);
        void AddPriceInfo(decimal currentPrice, int stockId, DateTime dateTime);
    }
}
