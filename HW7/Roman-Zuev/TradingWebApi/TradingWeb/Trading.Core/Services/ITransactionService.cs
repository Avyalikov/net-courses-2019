using Trading.Core.Dto;

namespace Trading.Core.Services
{
    public interface ITransactionService
    {
        void MakeTransaction(TransactionArguments args);
    }
}