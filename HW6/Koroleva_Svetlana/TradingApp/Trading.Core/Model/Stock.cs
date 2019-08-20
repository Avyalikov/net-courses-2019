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


        public enum StockType
    {
        Common,
        Preference
    }
    public class Stock

    {
        public int StockID { get; set; }
        public string StockPrefix { get; set; }
        public int IssuerID { get; set; }
        public StockType? StockType { get; set; }
        public virtual ICollection<PriceHistory> PriceHistories { get; set; }
        public virtual ICollection<ClientStock> ClientStocks { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
