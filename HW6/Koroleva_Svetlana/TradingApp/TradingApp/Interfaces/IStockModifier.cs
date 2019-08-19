// <copyright file="IStockModifier.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace TradingApp.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TradingApp.Model;

    /// <summary>
    /// IStockModifier description
    /// </summary>
    public interface IStockModifier
    {
        void AddStock(string prefix, int issuerID, StockType type);
        Stock GetStockByID(int id);
        Stock GetRandomStock();
       
    }
}
