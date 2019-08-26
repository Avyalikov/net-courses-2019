// <copyright file="Issuers.cs" company="SKorol">
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
    /// Issuers description
    /// </summary>
    public class Issuer
    {
        public int IssuerID { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
