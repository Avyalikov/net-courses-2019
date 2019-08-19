// <copyright file="IClientStocksModifier.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace TradingApp.Interfaces
{
    using TradingApp.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// IClientsStocksModifier description
    /// </summary>
    public interface IClientStocksModifier
    {
        void EditClientStocks(Order custOrder, Order salerOrder);
        bool CheckClientStock(int clientId, int stockId);
        void AddClientStock(int clientId, int stockId);
        ClientStock GetRandomClientStock(int clientID);
    }
}
