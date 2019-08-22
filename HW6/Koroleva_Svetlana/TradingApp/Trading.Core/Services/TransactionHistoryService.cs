using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Model;
using Trading.Core.Repositories;
using Trading.Core.DTO;



namespace Trading.Core.Services
{
    public class TransactionHistoryService
    {

        private ITableRepository<TransactionHistory> tableRepository;
      
        public TransactionHistoryService(ITableRepository<TransactionHistory> tableRepository)
        {
            this.tableRepository = tableRepository;
         
        }

        public void AddTransactionInfo(TransactionInfo args)
        {
            var transactionInfo = new TransactionHistory()
            {
                CustomerOrderID =args.CustomerOrderId,
                SalerOrderID =args.SalerOrderId,
                TransactionDateTime =args.TrDateTime
            };
            tableRepository.Add(transactionInfo);
            tableRepository.SaveChanges();

        }
        public TransactionHistory GetTransactionByID(int id)
        {
            if (!this.tableRepository.ContainsByPK(id))
            {
                throw new ArgumentException("Transaction doesn't exist");
            }
            return (TransactionHistory)this.tableRepository.FindByPK(id);
        }

       
    }
}
