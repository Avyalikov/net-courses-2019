namespace WebApiTradingServer.ODataControllers
{
    using System.Linq;
    using Microsoft.AspNet.OData;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Services;

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
            return this.clientManager.GetAllClients().AsQueryable();
        }

        [EnableQuery]
        public SingleResult<Client> Get([FromODataUri] int key)
        {
            var result = this.clientManager.GetAllClients().Where(c => c.ClientID == key).AsQueryable();
            return SingleResult.Create<Client>(result);
        }
    }
}
