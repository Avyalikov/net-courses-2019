// <copyright file="PriceInfo.cs" company="SKorol">
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
    /// PriceInfo description
    /// </summary>
    public class PriceInfo
    {
       public int StockId { get; set; }
       public DateTime DateTimeBegin { get; set; }
       public DateTime DateTimeEnd { get; set; }
       public decimal Price { get; set;}

    }
}
