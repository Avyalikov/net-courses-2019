using System;

namespace Trading.DataModel
{
    internal interface ITransactionHistoryRecorder
    {
        TransactionHistory RecordTransactionHistory(int sellerId, int buyerId, int sharesId, int quantity, DateTime TransactionDateTime);
    }
}