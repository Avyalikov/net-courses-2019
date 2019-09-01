using System.Collections;
using System.Collections.Generic;
using Trading.Core.Models;

namespace Trading.Core.Services
{
    public interface ITransactionHistoryService
    {
        ICollection<TransactionHistoryEntity> GetTopByClientId(int clientId, int top);
    }
}