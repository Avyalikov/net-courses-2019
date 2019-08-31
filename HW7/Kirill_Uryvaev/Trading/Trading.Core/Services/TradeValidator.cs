using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;
using Trading.Core.DataTransferObjects;
using Trading.Core.Repositories;

namespace Trading.Core
{
    public class TradeValidator : IValidator
    {
        private readonly IClientRepository clientsRepository;
        private readonly IClientsSharesRepository clientsSharesRepository;

        public TradeValidator(IClientRepository clientsRepository, IClientsSharesRepository clientsSharesRepository)
        {
            this.clientsRepository = clientsRepository;
            this.clientsSharesRepository = clientsSharesRepository;
        }

        public bool ValidateClientInfo(ClientRegistrationInfo clientInfo)
        {
            if (!clientInfo.FirstName.All(char.IsLetter) || !clientInfo.LastName.All(char.IsLetter))
            {
                return false;
            }
            if (!clientInfo.PhoneNumber.All(char.IsDigit))
            {
                return false;
            }

            return true;
        }

        public bool ValidateShareToClient(ClientsSharesInfo shareToClientInfo)
        {

            if (shareToClientInfo.ClientID < 0 && shareToClientInfo.ShareID < 0)
            {
                return false;
            }

            if (clientsRepository.LoadClientByID(shareToClientInfo.ClientID)==null)
            {
                return false;
            }

            var clientSharesInfo = new ClientsSharesEntity()
            {
                ClientID = shareToClientInfo.ClientID,
                ShareID = shareToClientInfo.ShareID
            };

            var clientsSharesEntity = clientsSharesRepository.LoadClientsSharesByID(clientSharesInfo);

            if (clientsSharesEntity != null)
            {
                if (clientsSharesEntity.Amount + shareToClientInfo.Amount <0)
                {
                    return false;
                }
            }
            else if (shareToClientInfo.Amount<0)
            {
                return false;
            }
            return true;
        }

        public bool ValidateClientMoney(int clientID, decimal amountOfMoney)
        {
            if (clientsRepository.LoadClientByID(clientID) == null)
            {
                return false;
            }
            
            return true;
        }

        public bool ValidateClientList(IEnumerable<ClientEntity> clients)
        {
            if (clients.Count() < 2)
            {
                return false;
            }
            return true;
        }

        public bool ValidateTradingClient(ClientEntity client)
        {
            if (client.ClientsShares.Count() < 1)
            {
                return false;
            }
            if (client.ClientsShares.Where(x=>x.Amount>0).Count()<1)
            {
                return false;
            }
            return true;
        }
    }
}
