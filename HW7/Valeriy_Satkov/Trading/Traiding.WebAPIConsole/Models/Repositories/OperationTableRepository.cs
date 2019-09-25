namespace Traiding.WebAPIConsole.Models.Repositories
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

        public void FillAllColumns(BlockedMoneyEntity blockedMoney, BlockedSharesNumberEntity blockedSharesNumber, DateTime chargeDate)
        {
            var operation = this.dBContext.Operations.First(o => o.Id == blockedMoney.Operation.Id); // it will fall here if we can't find
            operation.DebitDate = blockedMoney.CreatedAt;
            operation.Customer = blockedMoney.Customer;
            operation.Total = blockedMoney.Total;
            operation.Seller = blockedSharesNumber.Seller;
            operation.Share = blockedSharesNumber.Share;
            operation.ShareTypeName = blockedSharesNumber.ShareTypeName;
            operation.Cost = blockedSharesNumber.Cost;
            operation.Number = blockedSharesNumber.Number;
            operation.ChargeDate = chargeDate;
        }

        public IEnumerable<OperationEntity> GetByClient(int clientId, int number)
        {
            return this.dBContext.Operations.Where(o => o.Customer.Id == clientId).Take(number);
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
    }
}
