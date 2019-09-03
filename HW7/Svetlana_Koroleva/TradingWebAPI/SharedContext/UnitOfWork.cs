using System;
using System.Collections.Generic;
using System.Text;
using Trading.Core.Repositories;
using SharedContext.DAL;
using SharedContext.Repositories;
using Trading.Core;
using Microsoft.EntityFrameworkCore;

namespace SharedContext
{
   public class UnitOfWork:IUnitOfWork
    {
        private ExchangeContext db;

        private IClientRepository clientRepository;
        private IClientStockRepository clientStockRepository;
        private IOrderRepository orderRepository;
        private IStockRepository stockRepository;
        private ITransactionHistoryRepository transactionRepository;

        public UnitOfWork(ExchangeContext dbContext)
        {
            this.db = dbContext;
        }

        public IClientRepository Clients
        {
            get
            {
                if (clientRepository == null)
                {
                    clientRepository = new ClientRepository(db);
                }
                return clientRepository;
            }
        }

        public IClientStockRepository ClientStocks
        {
            get
            {
                if (clientStockRepository == null)
                {
                    clientStockRepository = new ClientStockRepository(db);
                }
                return clientStockRepository;
            }
        }

        public IOrderRepository Orders
        {
            get
            {
                if (orderRepository == null)
                {
                    orderRepository = new OrderRepository(db);
                }
                return orderRepository;
            }
        }


        public IStockRepository Stocks
        {
            get
            {
                if (stockRepository == null)
                {
                    stockRepository = new StockRepository(db);
                }
                return stockRepository;
            }
        }


        public ITransactionHistoryRepository Transactions
        {
            get
            {
                if (transactionRepository == null)
                {
                    transactionRepository = new TransactionHistoryRepository(db);
                }
                return transactionRepository;
            }
        }

      

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
