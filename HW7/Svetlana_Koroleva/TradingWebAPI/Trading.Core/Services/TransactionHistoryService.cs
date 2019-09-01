using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Model;
using Trading.Core.Repositories;
using Trading.Core.DTO;
using Trading.Core.IServices;



namespace Trading.Core.Services
{
    public class TransactionHistoryService:ITransactionHistoryService
    {

        private IUnitOfWork unitOfWork;
      
        public TransactionHistoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
         
        }

        public void AddTransactionInfo(TransactionInfo args)
        {
            var transactionInfo = new TransactionHistory()
            {
               
                TransactionDateTime =args.TrDateTime
            };
            unitOfWork.Transactions.Add(transactionInfo);
            unitOfWork.Save();

        }

        public TransactionHistory GetLastTransaction()
        {
            var transactions = this.unitOfWork.Transactions.GetAll().OrderByDescending(o => o.TransactionHistoryID);
            return transactions.First();
        }

        public TransactionHistory GetTransactionByID(int id)
        {
            if (this.unitOfWork.Transactions.Get(t=>t.TransactionHistoryID==id)==null)
            {
                throw new ArgumentException("Transaction doesn't exist");
            }
            return this.unitOfWork.Transactions.Get(t => t.TransactionHistoryID == id).Single();
        }

      
       
    }
}
