using stockSimulator.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace stockSimulator.Core.Services
{
    public class TransactionHistoryService
    {
        private readonly ITransactionHistoryTableRepository transactionHistoryTableRepository;

        public TransactionHistoryService(ITransactionHistoryTableRepository transactionHistoryTableRepository)
        {
            this.transactionHistoryTableRepository = transactionHistoryTableRepository;
        }

        public IEnumerable<HistoryEntity> GetClientsTransactions(int clientId, int top)
        {
            return this.transactionHistoryTableRepository.GetClientsTransactions(clientId, top);
        }
    }
}
