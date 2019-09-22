using HW7.Core.Dto;
using HW7.Core.Models;
using HW7.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Core.Services
{
    public class TransactionsService
    {
        private ITransactionsRepository transactionsRepository;
        private ITradersRepository tradersRepository;
        private ISharesRepository sharesRepository;
        private IPortfoliosRepository portfoliosRepository;

        public TransactionsService(ITransactionsRepository transactionsRepository, 
            ITradersRepository tradersRepository, ISharesRepository sharesRepository, IPortfoliosRepository portfoliosRepository)
        {
            this.transactionsRepository = transactionsRepository;
            this.tradersRepository = tradersRepository;
            this.sharesRepository = sharesRepository;
            this.portfoliosRepository = portfoliosRepository;
        }

        public IQueryable<Transaction> GetNumberTransactionsForTrader(int TraderId, int numberOfTransactions)
        {
            return transactionsRepository.GetNumberOfTransactionsForTrader(TraderId, numberOfTransactions);
        }

        public Transaction PerformNewDeal(TransactionToAdd transaction)
        {
            if (transaction == null || transaction.SellerId == 0 || transaction.BuyerId == 0 || transaction.ShareId == 0 || transaction.Quantity == 0 || transaction.SellerId == transaction.BuyerId)
            {
                return null;
            }

            var sellerToChange = tradersRepository.GetTrader(transaction.SellerId);
            var buyerToChange = tradersRepository.GetTrader(transaction.BuyerId);
            var sellerShareRecordToChange = portfoliosRepository.GetPortfolio(transaction.SellerId, transaction.ShareId);
            var sharePrice = sharesRepository.GetPrice(transaction.ShareId);

            if (sellerToChange == null || buyerToChange == null || sellerShareRecordToChange == null)
            {
                return null;
            }

            tradersRepository.AddToBalance(sellerToChange.TraderId, sharePrice * transaction.Quantity);
            tradersRepository.SubtractFromBalance(buyerToChange.TraderId, sharePrice * transaction.Quantity);

            portfoliosRepository.RemoveShares(sellerShareRecordToChange, transaction.Quantity);

            if (sellerShareRecordToChange.Quantity == 0)
            {
                portfoliosRepository.RemovePortfolio(sellerShareRecordToChange);
            }

            if (portfoliosRepository.GetPortfoliosCount(transaction.BuyerId, transaction.ShareId) > 0)
            {
                var buyerShareRecordToChange = portfoliosRepository.GetPortfolio(transaction.BuyerId, transaction.ShareId);

                if (buyerShareRecordToChange != null)
                {
                    portfoliosRepository.AddShares(buyerShareRecordToChange, transaction.Quantity);
                }
            }
            else
            {
                portfoliosRepository.AddPortfolio(transaction.BuyerId, transaction.ShareId, transaction.Quantity);
            }

            var newTransaction = transactionsRepository.AddTransaction(transaction.BuyerId, transaction.SellerId, transaction.ShareId, sharePrice, transaction.Quantity);

            return newTransaction;
        }

        public int GetShareQuantityFromPortfoio(int traderId, int shareId)
        {
            return portfoliosRepository.GetShareQuantityFromPortfoio(traderId, shareId);
        }
    }
}
