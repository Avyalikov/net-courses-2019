// <copyright file="ClientsStock.cs" company="SKorol">
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
    /// ClientsStock description
    /// </summary>
    public class ClientStock
    {
        
        public int ClientID { get; set; }
       
        public int StockID { get; set; }
        public int Quantity { get; set; }
    }
}
