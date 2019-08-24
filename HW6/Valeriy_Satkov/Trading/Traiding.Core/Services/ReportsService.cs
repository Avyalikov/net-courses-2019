namespace Traiding.Core.Services
{
    using System.Collections.Generic;    
    using Traiding.Core.Repositories;
    using Traiding.Core.Models;

    public class ReportsService
    {
        private IOperationTableRepository operationTableRepository;
        private ISharesNumberTableRepository sharesNumberTableRepository;
        private IBalanceTableRepository balanceTableRepository;

        public ReportsService(IOperationTableRepository operationTableRepository)
        {
            this.operationTableRepository = operationTableRepository;
        }        

        public ReportsService(ISharesNumberTableRepository sharesNumberTableRepository)
        {
            this.sharesNumberTableRepository = sharesNumberTableRepository;
        }

        public ReportsService(IBalanceTableRepository balanceTableRepository)
        {
            this.balanceTableRepository = balanceTableRepository;
        }

        public IEnumerable<OperationEntity> GetOperationByClient(int clientId)
        {
            return this.operationTableRepository.GetByClient(clientId);
        }

        public IEnumerable<SharesNumberEntity> GetSharesNumberByClient(int clientId)
        {
            return this.sharesNumberTableRepository.GetByClient(clientId);
        }

        public IEnumerable<SharesNumberEntity> GetSharesNumberByShare(int shareId)
        {
            return this.sharesNumberTableRepository.GetByShare(shareId);
        }

        public IEnumerable<BalanceEntity> GetZeroBalances()
        {
            return this.balanceTableRepository.GetZeroBalances();
        }

        public IEnumerable<BalanceEntity> GetNegativeBalances()
        {
            return this.balanceTableRepository.GetNegativeBalances();
        }
    }
}
