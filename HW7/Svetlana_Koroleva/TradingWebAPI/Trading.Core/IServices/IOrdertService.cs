// <copyright file="IOrderService.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.IServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.DTO;
    using Trading.Core.Model;

    /// <summary>
    /// IOrderService description
    /// </summary>
    public interface IOrderService
    {
        void AddOrder(OrderInfo args);
        Order GetEntityByID(int orderId);
        Order LastOrder();
        void SetIsExecuted(int orderid, int transactionId);
        void Delete(int id);
        void Update(int id, OrderInfo orderInfo);
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetTopOrdersForClient(int amount, int clientid);

    }
}
