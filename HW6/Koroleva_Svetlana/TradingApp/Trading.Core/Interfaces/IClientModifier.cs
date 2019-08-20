// <copyright file="IClientModifier.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Model;

    /// <summary>
    /// IClientModifier description
    /// </summary>
    public interface IClientModifier
    {
        void AddClient(string lastName, string firstName, string phone, decimal balance);
        Client GetClientByID(int id);
        Client GetRandomClient();
        void EditClientBalanse(Order custOrder, Order salerOrder, decimal price);
        IEnumerable<Client> GetClientsFromOrangeZone();
        IEnumerable<Client> GetClientsFromBlackZone();
    }
}
