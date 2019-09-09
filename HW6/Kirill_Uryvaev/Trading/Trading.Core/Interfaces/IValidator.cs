using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.DataTransferObjects;

namespace Trading.Core
{
    public interface IValidator
    {
        bool ValidateClientInfo(ClientRegistrationInfo clientInfo, ILogger logger);
        bool ValidateShareInfo(ShareRegistrationInfo shareInfo, ILogger logger);
        bool ValidateShareToClient(ClientsSharesInfo shareToClientInfo, ILogger logger);
        bool ValidateClientMoney(int clientID, int amountOfMoney, ILogger logger);
        bool ValidateClientList(IEnumerable<ClientEntity> clients, ILogger logger);
        bool ValidateTradingClient(ClientEntity client, ILogger logger);
    }
}
