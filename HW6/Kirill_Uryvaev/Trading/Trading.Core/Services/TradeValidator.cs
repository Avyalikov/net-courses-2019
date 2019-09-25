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
        private readonly IShareRepository shareRepository;
        private readonly IClientsSharesRepository clientsSharesRepository;

        public TradeValidator(IClientRepository clientsRepository, IShareRepository shareRepository, IClientsSharesRepository clientsSharesRepository)
        {
            this.clientsRepository = clientsRepository;
            this.shareRepository = shareRepository;
            this.clientsSharesRepository = clientsSharesRepository;
        }

        public bool ValidateClientInfo(ClientRegistrationInfo clientInfo, ILogger logger)
        {
            if (!clientInfo.FirstName.All(char.IsLetter) || !clientInfo.LastName.All(char.IsLetter))
            {
                logger.WriteWarn("Client's name contains forbidden symbols");
                return false;
            }
            if (!clientInfo.PhoneNumber.All(char.IsDigit))
            {
                logger.WriteWarn("Client's phone number contains forbidden symbols");
                return false;
            }

            return true;
        }

        public bool ValidateShareInfo(ShareRegistrationInfo shareInfo, ILogger logger)
        {
            if (shareInfo.Cost<1)
            {
                logger.WriteWarn("Share cannot have cost less than 1");
                return false;
            }
            return true;
        }

        public bool ValidateShareToClient(ClientsSharesInfo shareToClientInfo, ILogger logger)
        {

            if (shareToClientInfo.ClientID < 0 && shareToClientInfo.ShareID < 0)
            {
                logger.WriteWarn("ID cannot be less than 0");
                return false;
            }

            if (clientsRepository.LoadClientByID(shareToClientInfo.ClientID)==null)
            {
                logger.WriteWarn($"Client with ID {shareToClientInfo.ClientID} not exist");
                return false;
            }

            if (shareRepository.LoadShareByID(shareToClientInfo.ShareID) == null)
            {
                logger.WriteWarn($"Share with ID {shareToClientInfo.ShareID} not exist");
                return false;
            }

            var clientsSharesEntity = clientsSharesRepository.LoadClientsSharesByID(shareToClientInfo);

            if (clientsSharesEntity != null)
            {
                if (clientsSharesEntity.Amount + shareToClientInfo.Amount <0)
                {
                    logger.WriteWarn("Amount of shares cannot be less than 0");
                    return false;
                }
            }
            else if (shareToClientInfo.Amount<0)
            {
                logger.WriteWarn("Amount of shares cannot be less than 0");
                return false;
            }
            return true;
        }

        public bool ValidateClientMoney(int clientID, int amountOfMoney, ILogger logger)
        {
            if (clientsRepository.LoadClientByID(clientID) == null)
            {
                logger.WriteWarn("Client not exist");
                return false;
            }
            
            return true;
        }

        public bool ValidateClientList(IEnumerable<ClientEntity> clients, ILogger logger)
        {
            if (clients.Count() < 2)
            {
                logger.WriteWarn($"Not enough clients to trade");
                return false;
            }
            return true;
        }

        public bool ValidateTradingClient(ClientEntity client, ILogger logger)
        {
            if (client.ClientsShares.Count() < 1)
            {
                logger.WriteWarn($"{client.ClientID} has no shares");
                return false;
            }
            if (client.ClientsShares.Where(x=>x.Amount>0).Count()<1)
            {
                logger.WriteWarn($"{client.ClientID} has no shares");
                return false;
            }
            return true;
        }
    }
}
