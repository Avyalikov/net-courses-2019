using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;
using Traiding.Core.Repositories;

namespace Traiding.Core.Services
{
    public class OperationsService
    {
        private IOperationTableRepository tableRepository;

        public OperationsService(IOperationTableRepository operationTableRepository)
        {
            this.tableRepository = operationTableRepository;
        }

        public int Create()
        {
            var entityToAdd = new OperationEntity();            

            this.tableRepository.Add(entityToAdd);

            this.tableRepository.SaveChanges();

            return entityToAdd.Id;
        }
        public void ContainsById(int entityId)
        {
            if (!this.tableRepository.ContainsById(entityId))
            {
                throw new ArgumentException("Can't find operation with this Id. May it has not been registered.");
            }
        }

        public OperationEntity Get(int entityId)
        {
            ContainsById(entityId);

            return this.tableRepository.Get(entityId);
        }

        public IEnumerable<OperationEntity> GetByClient(int clientId)
        {
            return this.tableRepository.GetByClient(clientId);
        }

        public void FillCustomerColumns(int operationId, int blockedMoneyEntityId)
        {
            ContainsById(operationId);

            this.tableRepository.FillCustomerColumns(operationId, blockedMoneyEntityId);

            this.tableRepository.SaveChanges();
        }

        public void FillSellerColumns(int operationId, int blockedSharesNumberEntityId)
        {
            ContainsById(operationId);

            this.tableRepository.FillSellerColumns(operationId, blockedSharesNumberEntityId);

            this.tableRepository.SaveChanges();
        }
        
        public void SetChargeDate(int operationId)
        {
            ContainsById(operationId);

            this.tableRepository.SetChargeDate(operationId, DateTime.Now);

            this.tableRepository.SaveChanges();
        }


        public void Remove(int entityId)
        {
            ContainsById(entityId);

            this.tableRepository.Remove(entityId);

            this.tableRepository.SaveChanges();
        }
    }
}
