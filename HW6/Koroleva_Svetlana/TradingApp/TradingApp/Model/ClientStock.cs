// <copyright file="ClientsStock.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace TradingApp.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// ClientsStock description
    /// </summary>
    public class ClientStock
    {
        [Key]
        public int ClientID { get; set; }
        [Key]
        public int StockID { get; set; }
        public int Quantity { get; set; }
    }
}
