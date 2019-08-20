// <copyright file="PriceHistory.cs" company="SKorol">
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
    /// PriceHistory description
    /// </summary>
    public class PriceHistory
    {
        public int PriceHistoryID { get; set; }
        public int StockID { get; set; }
        public DateTime DateTimeBegin { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public decimal Price { get; set; }
    }
}
