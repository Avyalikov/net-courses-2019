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
    public class TransactionHistoryService
    {
        private readonly ITransactionHistoryRepository operationHistoryRepository;

        public TransactionHistoryService(ITransactionHistoryRepository operationHistoryRepository)
        {
            this.operationHistoryRepository = operationHistoryRepository;
        }

        public int Add(TransactionHistoryInfo operationHistoryInfo)
        {
            var operationHistory = new TransactionHistoryEntity()
            {
                BuyerClientID = operationHistoryInfo.BuyerClientID,
                SellerClientID = operationHistoryInfo.SellerClientID,
                Amount = operationHistoryInfo.Amount,
                ShareID = operationHistoryInfo.ShareID,
                SumOfOperation = operationHistoryInfo.SumOfOperation,
                DateTime = operationHistoryInfo.DateTime
            };
            operationHistoryRepository.Add(operationHistory);
            operationHistoryRepository.SaveChanges();
            return operationHistory.TransactionID;
        }

        public IEnumerable<TransactionHistoryEntity> GetOperationOfClient(int ID)
        {
            return operationHistoryRepository.LoadOperationsWithClientByID(ID);
        }

        public TransactionHistoryEntity GetOperation(int ID)
        {
            return operationHistoryRepository.LoadOperationByID(ID);
        }
    }
}
