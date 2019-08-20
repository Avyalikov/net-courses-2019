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
        public int IssuerId { get; set; }
        public StockType ShareType { get; set; }
    }
}
