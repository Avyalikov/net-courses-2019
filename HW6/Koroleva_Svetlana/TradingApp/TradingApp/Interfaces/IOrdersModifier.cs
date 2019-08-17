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
        void GenerateOrder(OrderType orderType);
        void AddOrder(Order order);
        Client SelectClient();
        Stock SelectStock();
        Order GetOrder(int id);
        int GetOrderId(Order order);
        void SetIsExecuted(Order custOrder, Order salerOrder);
        Order GetRandomSalerOrder();
        Order GetRandomCustomerOrder(Order order);
       
    }
}
