// <copyright file="PriceArguments.cs" company="SKorol">
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
    /// PriceArguments description
    /// </summary>
    public class PriceArguments
    {
        public int StockId { get; set; }
        public DateTime DateTimeLookUp { get; set; }
      
    }
}
