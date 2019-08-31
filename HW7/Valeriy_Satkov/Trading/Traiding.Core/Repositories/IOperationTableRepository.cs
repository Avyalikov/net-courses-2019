namespace Traiding.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using Traiding.Core.Models;

    public interface IOperationTableRepository
    {
        // bool Contains(OperationEntity entity);
        void Add(OperationEntity entity);
        void SaveChanges();
        // bool ContainsById(int entityId);
        // OperationEntity Get(int entityId);
        IEnumerable<OperationEntity> GetByClient(int clientId, int number);
        void FillAllColumns(BlockedMoneyEntity blockedMoney, BlockedSharesNumberEntity blockedSharesNumber, DateTime chargeDate);
        void Remove(int entityId);
        IEnumerable<OperationEntity> GetTopOperations(int v);
    }
}
