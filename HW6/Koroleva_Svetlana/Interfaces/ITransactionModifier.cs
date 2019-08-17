// <copyright file="ITransactionModifier.cs" company="SKorol">
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
    /// ITransaction description
    /// </summary>
    public interface ITransactionModifier
    {
        void CommitTransaction(Order custOrder, Order salerOrder, DateTime dateTime);
        int GetSalerOrderID(TransactionHistory transaction);
    }
}
