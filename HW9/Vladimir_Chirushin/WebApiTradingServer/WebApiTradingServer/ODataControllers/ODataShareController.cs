namespace WebApiTradingServer.ODataControllers
{
    using System.Linq;
    using Microsoft.AspNet.OData;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Services;

    public class ODataShareController : ODataController
    {
        private readonly IShareManager sharesManager;
        public ODataShareController(IShareManager sharesManager)
        {
            this.sharesManager = sharesManager;
        }

        [EnableQuery]
        public IQueryable<Share> Get()
        {
            return this.sharesManager.GetAllShares().AsQueryable();
        }

        [EnableQuery]
        public SingleResult<Share> Get([FromODataUri] int key)
        {
            IQueryable<Share> result = this.sharesManager.GetAllShares().Where(s => s.ShareID == key).AsQueryable();
            return SingleResult.Create<Share>(result);
        }
    }
}