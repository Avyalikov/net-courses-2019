namespace TradingApp.Core.Interfaces
{
    using DTO;
    using System.Collections.Generic;
    using Models;

    public interface IValidationService
    {
        bool ValidateClientRegistrationData(ClientRegistrationData clientData, ILogger logger);
        bool ValidateShareRegistrationData(ShareRegistrationData shareData, ILogger logger);
        bool ValidatePortfolioData(PortfolioData portfolioData, ILogger logger);
        bool ValidateClientData(Client client, ILogger logger);
        bool ValidateClientsAmount(IEnumerable<Client> clients, ILogger logger);
        bool ValidateTransaction(Transaction transaction, ILogger logger);
    }
}
