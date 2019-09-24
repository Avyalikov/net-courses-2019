using Microsoft.AspNet.OData;
using TradingSoftware.Core.Dto;
using TradingSoftware.Core.Services;

namespace WebApiTradingServer.ODataControllers
{
    class ODataBlockOfSharesController
    {
        private readonly IBlockOfSharesManager blockOfSharesManager;
        public ODataBlockOfSharesController(IBlockOfSharesManager blockOfSharesManager)
        {
            this.blockOfSharesManager = blockOfSharesManager;
        }

        [EnableQuery]
        public ClientShares Get([FromODataUri] int key)
        {
            var result = blockOfSharesManager.GetClientShares(key);
            return result;
        }
    }
}
