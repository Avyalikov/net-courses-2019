namespace TradingApp.Core.Services
{
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.ServicesInterfaces;

    public class TransactionServices : ITransactionServices
    {
        private readonly ITransactionTableRepository repo;

        public TransactionServices(ITransactionTableRepository repo)
        {
            this.repo = repo;
        }

        public int AddNewTransaction(TransactionStoryInfo args)
        {
            var TransToAdd = new TransactionStoryEntity()
            {
                AmountOfShares = args.AmountOfShares,
                CustomerId = args.customerId,
                SellerId = args.sellerId,
                ShareId = args.shareId,
                DateTime = args.DateTime,
                TransactionCost = args.TransactionCost,
                Share = args.Share
            };

            repo.Add(TransToAdd);

            repo.SaveChanges();

            return TransToAdd.Id;
        }
    }
}
