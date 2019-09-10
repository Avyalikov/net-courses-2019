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
        private IShareTableRepository shareTableRepository;
        private IClientTableRepository clientTableRepository;

        public ReportsService(IOperationTableRepository operationTableRepository, ISharesNumberTableRepository sharesNumberTableRepository, IBalanceTableRepository balanceTableRepository, IShareTableRepository shareTableRepository, IClientTableRepository clientTableRepository)
        {
            this.operationTableRepository = operationTableRepository;
            this.sharesNumberTableRepository = sharesNumberTableRepository;
            this.balanceTableRepository = balanceTableRepository;
            this.shareTableRepository = shareTableRepository;
            this.clientTableRepository = clientTableRepository;
        }

        public IEnumerable<OperationEntity> GetOperationByClient(int clientId, int number)
        {
            var result = this.operationTableRepository.GetByClient(clientId, number);
            if (result == null)
            {
                return new List<OperationEntity>();
            }
            return result;
        }

        public IEnumerable<SharesNumberEntity> GetSharesNumberByClient(int clientId)
        {
            var result = this.sharesNumberTableRepository.GetByClient(clientId);
            if (result == null)
            {
                return new List<SharesNumberEntity>();
            }
            return result;
        }

        public IEnumerable<SharesNumberEntity> GetSharesNumberByShare(int shareId)
        {
            var result = this.sharesNumberTableRepository.GetByShare(shareId);
            if (result == null)
            {
                return new List<SharesNumberEntity>();
            }
            return result;
        }

        public IEnumerable<string> GetZeroBalances()
        {
            var zeroBalances = this.balanceTableRepository.GetZeroBalances();
            if (zeroBalances == null)
            {
                return new List<string>();
            }

            var result = new List<string>();

            foreach (var zeroBalance in zeroBalances)
            {
                var client = zeroBalance.Client;
                result.Add(zeroBalance.Client.Id + zeroBalance.Client.LastName + zeroBalance.Client.FirstName);
            }

            return result;
        }

        public IEnumerable<BalanceEntity> GetNegativeBalances()
        {
            var result = this.balanceTableRepository.GetNegativeBalances();
            if (result == null)
            {
                return new List<BalanceEntity>();
            }
            return result;
        }

        public int GetClientsCount()
        {
            return this.clientTableRepository.GetClientsCount();
        }

        public int GetSharesCount()
        {
            return this.shareTableRepository.GetSharesCount();
        }

        //public IEnumerable<OperationEntity> GetTop10Operations()
        //{
        //    return this.operationTableRepository.GetTopOperations(10);
        //}

        public IEnumerable<ClientEntity> GetFirstClients(int number, int rank)
        {
            var result = this.clientTableRepository.Take(number, rank);
            if (result == null)
            {
                return new List<ClientEntity>();
            }
            return result;
        }
    }
}
