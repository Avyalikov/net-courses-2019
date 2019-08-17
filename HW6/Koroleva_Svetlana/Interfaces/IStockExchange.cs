// <copyright file="IStockExchande.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace ConsoleApp16.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ConsoleApp16.Model;

    /// <summary>
    /// IStockExchande description
    /// </summary>
    public interface IStockExchange
    {
       
        
        void EditClientBalanse(Order custOrder, Order salerOrder, decimal price);
        IEnumerable<Client> GetClientsFromOrangeZone();
        IEnumerable<Client> GetClientsFromBlackZone();
        void RunTraiding();
    }
}
