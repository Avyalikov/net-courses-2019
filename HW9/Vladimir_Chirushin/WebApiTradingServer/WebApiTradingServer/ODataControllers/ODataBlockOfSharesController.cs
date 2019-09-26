namespace WebApiTradingServer.ODataControllers
{
    using Microsoft.AspNet.OData;
    using TradingSoftware.Core.Dto;
    using TradingSoftware.Core.Services;

    public class ODataBlockOfSharesController
    {
        private readonly IBlockOfSharesManager blockOfSharesManager;
        public ODataBlockOfSharesController(IBlockOfSharesManager blockOfSharesManager)
        {
            this.blockOfSharesManager = blockOfSharesManager;
        }

        [EnableQuery]
        public ClientShares Get([FromODataUri] int key)
        {
            var result = this.blockOfSharesManager.GetClientShares(key);
            return result;
        }
    }
}
