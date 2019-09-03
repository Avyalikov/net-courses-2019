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
    using Trading.Core.Model;
    using Trading.Core.Repositories;
    using Trading.Core.DTO;
    using Trading.Core.IServices;

    /// <summary>
    /// IClientService description
    /// </summary>
    public interface ITransactionHistoryService
    {
        void AddTransactionInfo(TransactionInfo args);
        TransactionHistory GetTransactionByID(int id);
        TransactionHistory GetLastTransaction();
    }
}
