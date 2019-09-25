namespace TradeSimulator.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TradeSimulator.Core.Repositories;
    using TradeSimulator.Core.Dto;
    using TradeSimulator.Core.Models;

    public class TradingService
    {
        private readonly IAccountTableRepository accountTableRepository;
        private readonly IStockPriceTableRepository stockPriceTableRepository;
        private readonly IHistoryTableRepository historyTableRepository;
        private readonly IStockOfClientTableRepository stockOfClientTableRepository;
        private readonly ILogger logger;

        public TradingService(
            IAccountTableRepository accountTableRepository,
            IStockPriceTableRepository stockPriceTableRepository,
            IHistoryTableRepository historyTableRepository,
            IStockOfClientTableRepository stockOfClientTableRepository,
            ILogger logger)
        {
            this.accountTableRepository = accountTableRepository;
            this.stockPriceTableRepository = stockPriceTableRepository;
            this.historyTableRepository = historyTableRepository;
            this.stockOfClientTableRepository = stockOfClientTableRepository;
            this.logger = logger;
        }

        public void MakeATrade(TransactionInfo args)
        {
            logger.Info($"Start of transaction");
            if (string.IsNullOrEmpty(args.TypeOfStock))
            {
                throw new ArgumentException("This TransactionInfo doesnt contain this type of stock");
            };

            var stock = stockPriceTableRepository.GetStockPriceEntityByStockType(args.TypeOfStock);

            if (stock == null)
            {
                throw new ArgumentException("StockPrice Table doesnt contain this type of stock");
            };

            decimal fullPrice = stock.PriceOfStock * args.QuantityOfStocks;

            var buyerAccount = accountTableRepository.GetAccountByClientId(args.BuyerId);
            var sellerAccount = accountTableRepository.GetAccountByClientId(args.SellerId);

            if (buyerAccount == null || sellerAccount == null)
            {
                throw new ArgumentException("Account Table doesnt contain one of args Id");
            }

            logger.Info($"Buyer is client {buyerAccount.ClientId}");
            logger.Info($"Seller is client {sellerAccount.ClientId}");

            if (!sellerAccount.Stocks.Any(a => (a.TypeOfStocks == args.TypeOfStock) && (a.quantityOfStocks >= args.QuantityOfStocks)))
            {
                throw new ArgumentException("The Seller doesnt have enough stock of the required type");
            };
            buyerAccount.Balance -= fullPrice;
            logger.Info($"Buyer's balance {buyerAccount.Balance + fullPrice} -> {buyerAccount.Balance}");
            sellerAccount.Balance += fullPrice;
            logger.Info($"Seller's balance {sellerAccount.Balance - fullPrice} -> {sellerAccount.Balance}");

            var stockForTrade = sellerAccount.Stocks.FirstOrDefault(w => w.TypeOfStocks == args.TypeOfStock);
            int reminderStock = stockForTrade.quantityOfStocks - args.QuantityOfStocks;

            if (reminderStock == 0)
            {
                stockOfClientTableRepository.Delete(stockForTrade);
                stockOfClientTableRepository.SaveChanges();
            }
            else
            {
                stockOfClientTableRepository.GetStockOfClientEntityByAccountIdAndType(sellerAccount.AccountId, args.TypeOfStock).quantityOfStocks = reminderStock;
                stockOfClientTableRepository.SaveChanges();
            }
            logger.Info($"Seller's reminding stoks of type {args.TypeOfStock} is {reminderStock}");

            if (stockOfClientTableRepository.GetFullStockOfClientByAccountId(buyerAccount.AccountId).Any(a => a.TypeOfStocks == args.TypeOfStock))
            {
                stockOfClientTableRepository.GetStockOfClientEntityByAccountIdAndType(buyerAccount.AccountId, args.TypeOfStock).quantityOfStocks += args.QuantityOfStocks;
                stockOfClientTableRepository.SaveChanges();
            }
            else
            {
                stockOfClientTableRepository.Add(new StockOfClientEntity()
                {
                    AccountForStock = buyerAccount,
                    quantityOfStocks = args.QuantityOfStocks,
                    TypeOfStocks = args.TypeOfStock
                });
                stockOfClientTableRepository.SaveChanges();
            }
            logger.Info($"Buyer's reminding stoks of type {args.TypeOfStock} is {stockOfClientTableRepository.GetStockOfClientEntityByAccountIdAndType(buyerAccount.AccountId, args.TypeOfStock).quantityOfStocks}");

            accountTableRepository.Change(buyerAccount);
            accountTableRepository.Change(sellerAccount);
            accountTableRepository.SaveChanges();

            ChangeZoneIfRequiredByAccount(buyerAccount);
            logger.Info($"Changes are saved");
            WriteToHistory(args, fullPrice);
            logger.Info($"End of transaction");
        }

        private void WriteToHistory(TransactionInfo args, decimal fullPrice)
        {
            var entityToAdd = new HistoryEntity()
            {
                BuyerId = args.BuyerId,
                SellerId = args.SellerId,
                QuantityOfStocks = args.QuantityOfStocks,
                TypeOfStock = args.TypeOfStock,
                FullPrice = fullPrice
            };

            this.historyTableRepository.Add(entityToAdd);
            this.historyTableRepository.SaveChanges();
            logger.Info($"History record {entityToAdd.Id} of current transaction has been created");
        }

        private void ChangeZoneIfRequiredByAccount(AccountEntity account)
        {
            if (account.Balance == 0)
            {
                account.Zone = "orange";
            }
            if (account.Balance < 0)
            {
                account.Zone = "black";
            }
            logger.Info($"Zone of Buyer {account.ClientId} is checked");
            this.accountTableRepository.SaveChanges();
        }
    }
}
