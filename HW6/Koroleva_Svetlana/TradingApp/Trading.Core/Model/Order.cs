// <copyright file="Orders.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Orders description
    /// </summary>
    /// 

       public enum OrderType
    {
    Sale,
    Purchase
}

    public class Order
    {
        public int OrderID { get; set; }
        public int ClientID { get; set; }
        public int StockID { get; set; }
        public int Quantity { get; set; }
        public OrderType? OrderType { get; set; }
        public bool IsExecuted { get; set; }
    }
}
