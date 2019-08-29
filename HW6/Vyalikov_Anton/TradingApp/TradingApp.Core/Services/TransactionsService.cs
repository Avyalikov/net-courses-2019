namespace TradingApp.Core.Services
{
    using Interfaces;
    using DTO;
    using Models;
    using Repos;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TransactionsService : ITransactionsService
    {
        private readonly IClientService clientService;
        private readonly ISharesService sharesService;
        private readonly IPortfoliosService portfoliosService;
        private readonly ITransactionsRepository transactionsRepository;

        public TransactionsService(IClientService clientService, ISharesService sharesService, IPortfoliosService portfoliosService, ITransactionsRepository transactionsRepository)
        {
            this.clientService = clientService;
            this.sharesService = sharesService;
            this.portfoliosService = portfoliosService;
            this.transactionsRepository = transactionsRepository;
        }

        public void AddTransaction(Transactions transaction)
        {
            transactionsRepository.Insert(transaction);
        }

        public void SellOrBuyShares(Transactions transaction)
        {

            ClientsPortfolios portfolios = new ClientsPortfolios()
            {
                ClientID = transaction.SellerID,
                ShareID = transaction.ShareID,
                AmountOfShares = -transaction.AmountOfShares
            };

            if(transaction.SellerID == transaction.BuyerID)
            {
                throw new ArgumentException("Seller and buyer are the same person.");
            }

            portfoliosService.ChangeAmountOfShares(portfolios);
            portfolios.AmountOfShares *= -1;
            portfolios.ClientID = transaction.BuyerID;
            portfoliosService.ChangeAmountOfShares(portfolios);

            decimal sharePrice = sharesService.GetAllShares().Where(x => x.ShareID == transaction.ShareID).Select(x => x.Price).FirstOrDefault();
            clientService.ChangeBalance(transaction.SellerID, sharePrice * transaction.AmountOfShares);
            clientService.ChangeBalance(transaction.BuyerID, -(sharePrice * transaction.AmountOfShares));
        }

        public IEnumerable<Transactions> GetAllTransactions()
        {
            return transactionsRepository.GetAllTransactions();
        }
    }
}