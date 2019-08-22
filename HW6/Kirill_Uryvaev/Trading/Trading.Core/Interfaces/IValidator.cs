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
        bool ValidateClientInfo(ClientRegistrationInfo clientInfo);
        bool ValidateShareInfo(ShareRegistrationInfo shareInfo);
        bool ValidateShareToClient(ClientsSharesInfo shareToClientInfo);
        bool ValidateClientMoney(string[] clientInfo);
        bool ValidateClientList(IEnumerable<ClientEntity> clients);
        bool ValidateTradingClient(ClientEntity client);
    }
}
