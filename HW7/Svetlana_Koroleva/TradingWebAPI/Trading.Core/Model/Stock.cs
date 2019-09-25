// <copyright file="Stock.cs" company="SKorol">
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
    /// Stock description
    /// </summary>
    /// 

    public class Stock

    {
        public int StockID { get; set; }
        public string StockPrefix { get; set; }
        public string Issuer { get; set; }
        public string StockType { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<ClientStock> ClientStocks { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
