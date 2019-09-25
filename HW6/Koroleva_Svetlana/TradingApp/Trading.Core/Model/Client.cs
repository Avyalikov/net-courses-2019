// <copyright file="Clients.cs" company="SKorol">
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
    /// Clients description
    /// </summary>
    public class Client
    {
        public int ClientID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public decimal Balance { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public virtual ICollection<ClientStock> ClientStocks { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
