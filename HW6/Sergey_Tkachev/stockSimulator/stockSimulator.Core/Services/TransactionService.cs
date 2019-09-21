namespace stockSimulator.Core.Services
{
    using System;
    using stockSimulator.Core.DTO;
    using stockSimulator.Core.Repositories;

    public class TransactionService
    {
        private readonly IClientTableRepository clientTableRepository;
        private readonly IStockTableRepository stockTableRepository;
        private readonly IStockOfClientsTableRepository stockClientTableRepository;
        private readonly ITransactionHistoryTableRepository transactionHistoryTableRepository;
        private readonly EditCleintStockService editCleintStockService;

        /// <summary>
        /// Inializes an Instance of TransactionService class.
        /// </summary>
        /// <param name="clientTableRepository">Instance of implementing IClientTableRepository interface.</param>
        /// <param name="stockTableRepository">Instance of implementing IStockTableRepository interface.</param>
        /// <param name="stockClientTableRepository">Instance of implementing IStockOfClientsTableRepository interface.</param>
        /// <param name="transactionHistoryTableRepository">Instance of implementing ITransactionHistoryTableRepository interface.</param>
        /// <param name="editCleintStockService">Instance of EditCleintStockService class.</param>
        public TransactionService(
            IClientTableRepository clientTableRepository,
            IStockTableRepository stockTableRepository,
            IStockOfClientsTableRepository stockClientTableRepository,
            ITransactionHistoryTableRepository transactionHistoryTableRepository,
            EditCleintStockService editCleintStockService)
        {
            this.clientTableRepository = clientTableRepository;
            this.stockTableRepository = stockTableRepository;
            this.stockClientTableRepository = stockClientTableRepository;
            this.transactionHistoryTableRepository = transactionHistoryTableRepository;
            this.editCleintStockService = editCleintStockService;
        }

        /// <summary>
        /// Runs the trade process.
        /// </summary>
        /// <param name="tradeInfo">Instance with trade info.</param>
        public void Trade(TradeInfo tradeInfo)
        {
            this.ValidateTransactionByArguments(tradeInfo);
            this.BuyPartOfTrasaction(tradeInfo);
            this.SellPartOfTrasaction(tradeInfo);
            this.AddEntryToTransactionHistoryTable(tradeInfo);
        }

        /// <summary>
        /// Adds entry into TransactionHistoryTable.
        /// </summary>
        /// <param name="tradeInfo">Instance with trade info.</param>
        private void AddEntryToTransactionHistoryTable(TradeInfo tradeInfo)
        {
            decimal stockCost = this.stockTableRepository.GetCost(tradeInfo.Stock_ID);
            decimal transactionPrice = stockCost * tradeInfo.Amount;
            string stockType = this.stockTableRepository.GetType(tradeInfo.Stock_ID);

            HistoryEntity historyEntity = new HistoryEntity()
            {
                CustomerID = tradeInfo.Customer_ID,
                StockAmount = tradeInfo.Amount,
                StockID = tradeInfo.Stock_ID,
                SellerID = tradeInfo.Seller_ID,
                TransactionTime = DateTime.Now,
                TransactionCost = transactionPrice,
                StockType = stockType
            };

            this.transactionHistoryTableRepository.Add(historyEntity);
            this.transactionHistoryTableRepository.SaveChanges();
        }

        /// <summary>
        /// Reduses customer's money and add him seller's stocks.
        /// </summary>
        /// <param name="tradeInfo">Instance with trade info.</param>
        private void BuyPartOfTrasaction(TradeInfo tradeInfo)
        {
            decimal stockCost = this.stockTableRepository.GetCost(tradeInfo.Stock_ID);
            int customerStocks = this.stockClientTableRepository.GetAmount(tradeInfo.Customer_ID, tradeInfo.Stock_ID);
           
            decimal customerMoney = this.clientTableRepository.GetBalance(tradeInfo.Customer_ID);

            decimal transactionPrice = stockCost * tradeInfo.Amount;
            decimal newCustomerBalance = customerMoney - transactionPrice;

            int newCustomerStockAmount = customerStocks + tradeInfo.Amount;

            this.clientTableRepository.UpdateBalance(tradeInfo.Customer_ID, newCustomerBalance);

            this.editCleintStockService.Edit(new EditStockOfClientInfo
            {
                Client_ID = tradeInfo.Customer_ID,
                Stock_ID = tradeInfo.Stock_ID,
                AmountOfStocks = newCustomerStockAmount
            });

            this.clientTableRepository.SaveChanges();
        }

        /// <summary>
        /// Reduses seller's stocks and adds him customer's money.
        /// </summary>
        /// <param name="tradeInfo">Inctance with trade info.</param>
        private void SellPartOfTrasaction(TradeInfo tradeInfo)
        {
            decimal stockCost = this.stockTableRepository.GetCost(tradeInfo.Stock_ID);
            decimal transactionPrice = stockCost * tradeInfo.Amount;
            decimal sellerMoney = this.clientTableRepository.GetBalance(tradeInfo.Seller_ID);
            decimal newSellerBalance = sellerMoney + transactionPrice;
            this.clientTableRepository.UpdateBalance(tradeInfo.Seller_ID, newSellerBalance);
            int sellerStocks = this.stockClientTableRepository.GetAmount(tradeInfo.Seller_ID, tradeInfo.Stock_ID);
            int newSellerStockAmount = sellerStocks - tradeInfo.Amount;
            this.editCleintStockService.Edit(new EditStockOfClientInfo
            {
                Client_ID = tradeInfo.Seller_ID,
                Stock_ID = tradeInfo.Stock_ID,
                AmountOfStocks = newSellerStockAmount
            });

            this.clientTableRepository.SaveChanges();
        }

        /// <summary>
        /// Validates transactions arguments.
        /// </summary>
        /// <param name="tradeInfo">Instance with trade info.</param>
        private void ValidateTransactionByArguments(TradeInfo tradeInfo)
        {
            int sellerStocks = this.stockClientTableRepository.GetAmount(tradeInfo.Seller_ID, tradeInfo.Stock_ID);
            
            if (sellerStocks < tradeInfo.Amount)
            {
                throw new ArgumentException($@"Can't handle this transaction: Client with {tradeInfo.Seller_ID} ID
has only {sellerStocks} stocks with {tradeInfo.Stock_ID} ID, but requested {tradeInfo.Amount}.");
            }
        }
    }
}
