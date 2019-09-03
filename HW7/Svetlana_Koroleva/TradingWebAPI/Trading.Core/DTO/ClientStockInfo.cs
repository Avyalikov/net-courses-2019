// <copyright file="ClientStockInfo.cs" company="SKorol">
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
    /// ClientStockInfo description
    /// </summary>
    public class ClientStockInfo
    {
        public int ClientId { get; set; }
        public int StockId { get; set; }
       public int Amount { get; set; }
    }
}
