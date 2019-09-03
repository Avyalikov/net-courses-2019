// <copyright file="IUnitOfWork.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Repositories;

    /// <summary>
    /// IUnitOfWork description
    /// </summary>
    public interface IUnitOfWork
    {
        IClientRepository Clients { get; }
        IClientStockRepository ClientStocks { get; }
        IOrderRepository Orders{ get; }
        IStockRepository Stocks { get; }
        ITransactionHistoryRepository Transactions { get; }
        void Save();
    }
}
