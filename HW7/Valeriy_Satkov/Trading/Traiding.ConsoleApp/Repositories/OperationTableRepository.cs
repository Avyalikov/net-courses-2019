namespace Traiding.ConsoleApp.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;

    public class OperationTableRepository : IOperationTableRepository
    {
        private readonly StockExchangeDBContext dBContext;

        public OperationTableRepository(StockExchangeDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public void Add(OperationEntity entity)
        {
            this.dBContext.Operations.Add(entity);
        }

        public void FillCustomerColumns(BlockedMoneyEntity blockedMoney)
        {
            var operation = this.dBContext.Operations.First(o => o.Id == blockedMoney.Operation.Id); // it will fall here if we can't find
            operation.DebitDate = blockedMoney.CreatedAt;
            operation.Customer = blockedMoney.Customer;
            operation.Total = blockedMoney.Total;
        }

        public void FillSellerColumns(BlockedSharesNumberEntity blockedSharesNumber)
        {
            var operation = this.dBContext.Operations.First(o => o.Id == blockedSharesNumber.Operation.Id); // it will fall here if we can't find
            operation.Seller = blockedSharesNumber.Seller;
            operation.Share = blockedSharesNumber.Share;
            operation.ShareTypeName = blockedSharesNumber.ShareTypeName;
            operation.Cost = blockedSharesNumber.Cost;
            operation.Number = blockedSharesNumber.Number;
        }

        public IEnumerable<OperationEntity> GetByClient(int clientId)
        {
            return this.dBContext.Operations.Where(o => o.Customer.Id == clientId);
        }

        public IEnumerable<OperationEntity> GetTopOperations(int number)
        {
            return this.dBContext.Operations.Take(number);
        }

        public void Remove(int entityId)
        {
            var operation = this.dBContext.Operations.First(o => o.Id == entityId); // it will fall here if we can't find
            this.dBContext.Operations.Remove(operation);
        }

        public void SaveChanges()
        {
            this.dBContext.SaveChanges();
        }

        public void SetChargeDate(int entityId, DateTime chargeDate)
        {
            var operation = this.dBContext.Operations.First(n => n.Id == entityId); // it will fall here if we can't find
            operation.ChargeDate = chargeDate;
        }
    }
}
