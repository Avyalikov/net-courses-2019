namespace TradingApp.Core.Services
{
    using DTO;
    using Interfaces;
    using Models;
    using Repos;
    using System.Collections.Generic;
    using System.Linq;

    public class ValidationService : IValidationService
    {
        private readonly IClientRepository clientRepository;
        private readonly ISharesRepository sharesRepository;
        private readonly IPortfolioRepository portfolioRepository;

        public ValidationService(IClientRepository clientRepository, ISharesRepository sharesRepository, IPortfolioRepository portfolioRepository)
        {
            this.clientRepository = clientRepository;
            this.sharesRepository = sharesRepository;
            this.portfolioRepository = portfolioRepository;
        }

        public bool ValidateClientRegistrationData(ClientRegistrationData clientData, ILogger logger)
        {
            if (!clientData.ClientName.Replace(" ", "").All(char.IsLetter))
            {
                logger.WriteWarning("Client's name and last name must contain only letters.");
                return false;
            }

            if (!clientData.ClientPhone.All(char.IsDigit))
            {
                logger.WriteWarning("Client's phone number must contain only digits.");
                return false;
            }

            return true;
        }

        public bool ValidateShareRegistrationData(ShareRegistrationData shareData, ILogger logger)
        {
            if (shareData.SharePrice < 1)
            {
                logger.WriteWarning("Share can't costs less, than 1 dollar.");
                return false;
            }
            return true;
        }

        public bool ValidatePortfolioData(PortfolioData portfolioData, ILogger logger)
        {
            var clientPortfolio = portfolioRepository.GetPortfolioByClientID(portfolioData.ClientID);

            if (clientPortfolio != null || portfolioData.AmountOfShares < 0)
            {
                if (clientPortfolio.AmountOfShares + portfolioData.AmountOfShares < 0)
                {
                    logger.WriteWarning("Amount of shares can't be less, than 0.");
                    return false;
                }
            }
            return true;
        }

        public bool ValidateClientData(Client client, ILogger logger)
        {
            if (client.ClientID < 0)
            {
                logger.WriteWarning("Client ID can't be less, than 0.");
                return false;
            }
            return true;
        }

        public bool ValidateClientsAmount(IEnumerable<Client> clients, ILogger logger)
        {
            if (clients.Count() < 2)
            {
                logger.WriteWarning($"There are less, than 2 traders, that's why trading is impossible.");
                return false;
            }
            return true;
        }

        public bool ValidateTransaction(Transaction transaction, ILogger logger)
        {
            if (portfolioRepository.GetPortfolioByClientID(transaction.SellerID).AmountOfShares < transaction.AmountOfShares)
            {
                logger.WriteWarning($"{transaction.SellerID} has not enough shares to make this transaction.");
                return false;
            }

            if (clientRepository.GetClientBalance(transaction.BuyerID) <= 0)
            {
                logger.WriteWarning($"{transaction.BuyerID} has no money and impossible to trade with.");
                return false;
            }

            return true;
        }
    }
}
