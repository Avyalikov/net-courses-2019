// <copyright file="IOrdersModifier.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace TradingApp.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TradingApp.DAL;
    using TradingApp.Model;

    /// <summary>
    /// IOrdersModifier description
    /// </summary>
    public interface IOrderModifier
    {
        void AddOrder(OrderType orderType, int stockId, int clientId, int amount);
        Order GetOrder(int id);
        int GetOrderId(Order order);
        void SetIsExecuted(Order custOrder, Order salerOrder);
        Order GetRandomCustomerOrder(Order order);
        bool IsExists(int clientId, int stockId, int amount, OrderType orderType, bool isExecuted);
       

    }
}
