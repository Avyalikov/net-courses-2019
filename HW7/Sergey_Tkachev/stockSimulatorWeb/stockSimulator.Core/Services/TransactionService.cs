using System;
using stockSimulator.Core.DTO;
using stockSimulator.Core.Models;
using stockSimulator.Core.Repositories;

namespace stockSimulator.Core.Services
{
    public class TransactionService
    {
        private readonly IClientTableRepository clientTableRepository;
        private readonly IStockTableRepository stockTableRepository;
        private readonly IStockOfClientsTableRepository stockClientTableRepository;
        private readonly ITransactionHistoryTableRepository transactionHistoryTableRepository;
        private readonly EditCleintStockService editCleintStockService;

        public TransactionService(IClientTableRepository clientTableRepository,
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

        public void Trade(TradeInfo tradeInfo)
        {
            this.ValidateTransactionByArguments(tradeInfo);
            this.BuyPartOfTrasaction(tradeInfo);
            this.SellPartOfTrasaction(tradeInfo);
            this.AddEntryToTransactionHistoryTable(tradeInfo);
        }

        private void AddEntryToTransactionHistoryTable(TradeInfo tradeInfo)
        {
            decimal stockCost = stockTableRepository.GetCost(tradeInfo.Stock_ID);
            decimal transactionPrice = stockCost * tradeInfo.Amount;
            string stockType = stockTableRepository.GetType(tradeInfo.Stock_ID);

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
            transactionHistoryTableRepository.SaveChanges();
        }

        private void BuyPartOfTrasaction(TradeInfo tradeInfo)
        {
            decimal stockCost = stockTableRepository.GetCost(tradeInfo.Stock_ID);
            int customerStocks = stockClientTableRepository.GetAmount(tradeInfo.Customer_ID, tradeInfo.Stock_ID);
           
            decimal customerMoney = clientTableRepository.GetBalance(tradeInfo.Customer_ID);

            decimal transactionPrice = stockCost * tradeInfo.Amount;
            decimal newCustomerBalance = customerMoney - transactionPrice;

            int newCustomerStockAmount = customerStocks + tradeInfo.Amount;

            clientTableRepository.UpdateBalance(tradeInfo.Customer_ID, newCustomerBalance);

            editCleintStockService.Edit(new EditStockOfClientInfo
            {
                Client_ID = tradeInfo.Customer_ID,
                Stock_ID = tradeInfo.Stock_ID,
                AmountOfStocks = newCustomerStockAmount
            });
            clientTableRepository.SaveChanges();
        }

        private void SellPartOfTrasaction(TradeInfo tradeInfo)
        {
            decimal stockCost = stockTableRepository.GetCost(tradeInfo.Stock_ID);
            decimal transactionPrice = stockCost * tradeInfo.Amount;
            decimal sellerMoney = clientTableRepository.GetBalance(tradeInfo.Seller_ID);
            decimal newSellerBalance = sellerMoney + transactionPrice;
            clientTableRepository.UpdateBalance(tradeInfo.Seller_ID, newSellerBalance);
            int sellerStocks = stockClientTableRepository.GetAmount(tradeInfo.Seller_ID, tradeInfo.Stock_ID);
            int newSellerStockAmount = sellerStocks - tradeInfo.Amount;
            editCleintStockService.Edit(new EditStockOfClientInfo
            {
                Client_ID = tradeInfo.Seller_ID,
                Stock_ID = tradeInfo.Stock_ID,
                AmountOfStocks = newSellerStockAmount
            });
            clientTableRepository.SaveChanges();
        }

        private void ValidateTransactionByArguments(TradeInfo tradeInfo)
        {
            int sellerStocks = stockClientTableRepository.GetAmount(tradeInfo.Seller_ID, tradeInfo.Stock_ID);


            if (sellerStocks < tradeInfo.Amount)
            {
                throw new ArgumentException($@"Can't handle this transaction: Client with {tradeInfo.Seller_ID} ID
has only {sellerStocks} stocks with {tradeInfo.Stock_ID} ID, but requested {tradeInfo.Amount}.");
            }
           
        }
    }
}
