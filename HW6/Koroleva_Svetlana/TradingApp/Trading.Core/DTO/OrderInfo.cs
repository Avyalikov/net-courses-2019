// <copyright file="OrderInfo.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// OrderInfo description
    /// </summary>
    public class OrderInfo
    {
        public enum OrdType
        {
            Sale,
            Purchase
        }

        public int ClientId { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
        public OrdType OrderType { get; set; }
        public bool IsExecuted { get; set; }
    }
}
