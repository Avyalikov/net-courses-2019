// <copyright file="IClientsTableRep.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Model;

    /// <summary>
    /// IClientsTableRep description
    /// </summary>
    public interface IClientTableRepository
    {
        void Add(Client client);
        bool ContainsByID(int clientId);
        Client GetClientByID(int clientId);
        IEnumerable<Type> FindClientsByRequest(string arguments);
        void SaveChanges();
       
    }
}
