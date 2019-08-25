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
        IEnumerable<OperationEntity> GetByClient(int clientId);
        void FillCustomerColumns(BlockedMoneyEntity blockedMoney); // DateTime DebitDate, ClientEntity Customer, decimal Total 
        void FillSellerColumns(BlockedSharesNumberEntity blockedSharesNumber); // ClientEntity Seller, ShareEntity Share, string ShareTypeName, decimal Cost, int Number
        void SetChargeDate(int entityId, DateTime chargeDate);
        void Remove(int entityId);
        IEnumerable<OperationEntity> GetTopOperations(int v);
    }
}
