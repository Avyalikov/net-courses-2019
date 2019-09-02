// <copyright file="IClientStockService.cs" company="SKorol">
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
    /// IClientStockService description
    /// </summary>
    public interface IClientStockService
    { 
        void AddClientStockToDB(ClientStockInfo args);
        void Delete(int clientId, int stockId);
        void Update(int clientId, int stockId, ClientStockInfo clientStockInfo);
        ClientStock GetEntityByCompositeID(int clientId, int stockId);
        void EditClientStocksAmount(int clientId, int stockId, int amountToAdd);
        IQueryable GetClientStocksWithPrice(int clientId);
        IEnumerable<ClientStock> GetclientStocks(int clientId);


    }
}
