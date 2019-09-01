// <copyright file="TransactionHistory.cs" company="SKorol">
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
    /// TransactionHistory description
    /// </summary>
    public class TransactionHistory
    {
        public int TransactionHistoryID { get; set; }
        public DateTime TransactionDateTime{get;set;}
       // public virtual ICollection<Order> Orders { get; set; }
    }
}
