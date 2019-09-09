// <copyright file="IStockService.cs" company="SKorol">
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
    /// IStockService description
    /// </summary>
    public interface IStockService
    {
        void AddStock(StockInfo args);
        void Delete(int stockId);
        void Update(int stockId, StockInfo args);
        Stock GetEntityByID(int id);



    }
}
