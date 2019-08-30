using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Repositories;
using Trading.Core.DataTransferObjects;
using Trading.Core;

namespace Trading.Core.Services
{
    public class OperationHistoryService
    {
        private readonly IOperationHistoryRepository operationHistoryRepository;

        public OperationHistoryService(IOperationHistoryRepository operationHistoryRepository)
        {
            this.operationHistoryRepository = operationHistoryRepository;
        }

        public int Add(OperationHistoryInfo operationHistoryInfo)
        {
            var operationHistory = new OperationHistoryEntity()
            {
                BuyerClientID = operationHistoryInfo.BuyerClientID,
                SellerClientID = operationHistoryInfo.SellerClientID,
                Amount = operationHistoryInfo.Amount,
                ShareID = operationHistoryInfo.ShareID,
                SumOfOperation = operationHistoryInfo.SumOfOperation
            };
            operationHistoryRepository.Add(operationHistory);
            operationHistoryRepository.SaveChanges();
            return operationHistory.OperationID;
        }

        public IEnumerable<OperationHistoryEntity> GetOperationOfClient(int ID)
        {
            return operationHistoryRepository.LoadOperationsWithClientByID(ID);
        }

        public OperationHistoryEntity GetOperation(int ID)
        {
            return operationHistoryRepository.LoadOperationByID(ID);
        }
    }
}
