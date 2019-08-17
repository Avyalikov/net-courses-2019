// <copyright file="TransactionHistory.cs" company="SKorol">
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
    /// TransactionHistory description
    /// </summary>
    public class TransactionHistory
    {
        public int TransactionHistoryID { get; set; }
        public int CustomerOrderID { get; set; } 
        public int SalerOrderID { get; set; }
        public DateTime DateTime{get;set;}
    }
}
