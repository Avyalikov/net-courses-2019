using System;
using System.Collections;
using System.Collections.Generic;
using Trading.Core.Models;
using Trading.Core.Repositories;

namespace Trading.Core.Services
{
    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly ITransactionHistoryTableRepository transactionHistoryTableRepository;

        public TransactionHistoryService(ITransactionHistoryTableRepository transactionHistoryTableRepository)
        {
            this.transactionHistoryTableRepository = transactionHistoryTableRepository;
        }
        public ICollection<TransactionHistoryEntity> GetTransactions(int clientId, int top)
        {
            return transactionHistoryTableRepository.GetTransactionsById(clientId, top);
        }
    }
}
