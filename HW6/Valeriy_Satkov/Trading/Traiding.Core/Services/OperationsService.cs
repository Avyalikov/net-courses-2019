namespace Traiding.Core.Services
{
    using System.Collections.Generic;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;
    //using System;
    //using System.Linq;
    //using System.Text;
    //using System.Threading.Tasks;

    public class OperationsService
    {
        private IOperationTableRepository tableRepository;

        public OperationsService(IOperationTableRepository operationTableRepository)
        {
            this.tableRepository = operationTableRepository;
        }

        /* Implemented in Sale service
         */
        //public int Create() // it have implementation in sale service
        //{
        //    var entityToAdd = new OperationEntity();

        //    this.tableRepository.Add(entityToAdd);

        //    this.tableRepository.SaveChanges();

        //    return entityToAdd.Id;
        //}
        //public void ContainsById(int entityId)
        //{
        //    if (!this.tableRepository.ContainsById(entityId))
        //    {
        //        throw new ArgumentException("Can't find operation with this Id. May it has not been registered.");
        //    }
        //}

        //public OperationEntity Get(int entityId)
        //{
        //    ContainsById(entityId);

        //    return this.tableRepository.Get(entityId);
        //}

        public IEnumerable<OperationEntity> GetByClient(int clientId)
        {
            return this.tableRepository.GetByClient(clientId);
        }

        /* Implemented in Sale service
         */
        //public void FillCustomerColumns(int operationId, int blockedMoneyEntityId)
        //{
        //    ContainsById(operationId);

        //    this.tableRepository.FillCustomerColumns(operationId, blockedMoneyEntityId);

        //    this.tableRepository.SaveChanges();
        //}

        //public void FillSellerColumns(int operationId, int blockedSharesNumberEntityId)
        //{
        //    ContainsById(operationId);

        //    this.tableRepository.FillSellerColumns(operationId, blockedSharesNumberEntityId);

        //    this.tableRepository.SaveChanges();
        //}

        //public void SetChargeDate(int operationId)
        //{
        //    ContainsById(operationId);

        //    this.tableRepository.SetChargeDate(operationId, DateTime.Now);

        //    this.tableRepository.SaveChanges();
        //}

        //public void Remove(int entityId)
        //{
        //    ContainsById(entityId);

        //    this.tableRepository.Remove(entityId);

        //    this.tableRepository.SaveChanges();
        //}
    }
}
