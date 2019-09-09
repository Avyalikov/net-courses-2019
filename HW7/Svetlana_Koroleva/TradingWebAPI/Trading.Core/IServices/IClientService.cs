// <copyright file="IClientService.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.IServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.DTO;
    using Trading.Core.Model;

    /// <summary>
    /// IClientService description
    /// </summary>
    public interface IClientService
    {

        void AddClientToDB(ClientInfo args);
        void Delete(int clientId);
        void Update(int clientId, ClientInfo args);
        Client GetEntityByID(int clientId);
        void EditClientBalance(int clientId, decimal sumToAdd);
        IEnumerable<Client> GetClientsFromOrangeZone();
        IEnumerable<Client> GetClientsFromBlackZone();
        IEnumerable<Client> GetAllClients();
        IEnumerable<Client> GetTopClients(int amount);
        decimal GetClientBalance(int clientId);
        string GetClientStatus(int clientId);

    }
}
