// <copyright file="StockInfo.cs" company="SKorol">
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
    /// StockInfo description
    /// </summary>
    public class StockInfo
    {
        public enum StockType
        {
            Common,
            Preference
        }

        public string StockPrefix { get; set; }
        public string Issuer { get; set; }
        public string ShareType { get; set; }
        public decimal Price { get; set; }
    }
}
