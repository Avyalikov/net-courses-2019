using Microsoft.AspNet.OData;
using System.Linq;
using TradingSoftware.Core.Models;
using TradingSoftware.Core.Services;

namespace WebApiTradingServer.ODataControllers
{
    public class ODataClientsController : ODataController
    {
        private readonly IClientManager clientManager;
        public ODataClientsController(IClientManager clientManager)
        {
            this.clientManager = clientManager;
        }
        [EnableQuery]
        public IQueryable<Client> Get()
        {
            return clientManager.GetAllClients().AsQueryable();
        }

        [EnableQuery]
        public SingleResult<Client> Get([FromODataUri] int key)
        {
            var result = clientManager.GetAllClients().Where(c => c.ClientID == key).AsQueryable();
            return SingleResult.Create<Client>(result);
        }
    }
}
