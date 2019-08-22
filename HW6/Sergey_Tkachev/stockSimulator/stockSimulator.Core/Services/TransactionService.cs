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

        public TransactionService(IClientTableRepository clientTableRepository,
                                  IStockTableRepository stockTableRepository,
                                  IStockOfClientsTableRepository stockClientTableRepository)
        {
            this.clientTableRepository = clientTableRepository;
            this.stockTableRepository = stockTableRepository;
            this.stockClientTableRepository = stockClientTableRepository;
        }

        public void Trade(TradeInfo tradeInfo)
        {
            decimal stockCost = stockTableRepository.GetCost(tradeInfo.Stock_ID);
            int customerStocks = stockClientTableRepository.GetAmount(tradeInfo.Customer_ID, tradeInfo.Stock_ID);
            int sellerStocks = stockClientTableRepository.GetAmount(tradeInfo.Seller_ID, tradeInfo.Stock_ID);
            decimal customerMoney = clientTableRepository.GetBalance(tradeInfo.Customer_ID);
            decimal sellerMoney = clientTableRepository.GetBalance(tradeInfo.Seller_ID);

            decimal transactionPrice = stockCost * tradeInfo.Amount;
            decimal newCustomerBalance = customerMoney - transactionPrice;
            decimal newSellerBalance = sellerMoney + transactionPrice;

            int newCustomerStockAmount = customerStocks + tradeInfo.Amount;
            int newSellerStockAmount = sellerStocks - tradeInfo.Amount;


            clientTableRepository.UpdateBalance(tradeInfo.Customer_ID, newCustomerBalance);
            clientTableRepository.UpdateBalance(tradeInfo.Seller_ID, newSellerBalance);

            stockClientTableRepository.UpdateAmount(tradeInfo.Seller_ID,
                                                    tradeInfo.Stock_ID,
                                                    newCustomerStockAmount);
            stockClientTableRepository.UpdateAmount(tradeInfo.Customer_ID,
                                                    tradeInfo.Stock_ID,
                                                    newCustomerStockAmount);
        }


    }
}
