using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Model;
using Trading.Core.Repositories;
using Trading.Core.DTO;



namespace Trading.Core.Modifiers
{
    class TransactionHistoryService
    {

        private ITableRepository tableRepository;
        private readonly IOnePKTableRepository onePKTableRepository;
        public TransactionHistoryService(ITableRepository tableRepository, IOnePKTableRepository onePKTableRepository)
        {
            this.tableRepository = tableRepository;
            this.onePKTableRepository = onePKTableRepository;
        }

        public void AddTransactionInfo(TransactionInfo args)
        {
            var transactionInfo = new TransactionHistory() { CustomerOrderID=args.CustomerOrderId, SalerOrderID=args.SalerOrderId, TransactionDateTime=args.TrDateTime};
            tableRepository.Add(transactionInfo);
            tableRepository.SaveChanges();

        }
        public TransactionHistory GetTransactionByID(int id)
        {
            if (!this.onePKTableRepository.ContainsByID(id))
            {
                throw new ArgumentException("Transaction doesn't exist");
            }
            return (TransactionHistory)this.onePKTableRepository.GetEntityByID(id);
        }

       
    }
}
