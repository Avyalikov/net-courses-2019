using Microsoft.AspNet.OData;
using System.Linq;
using TradingSoftware.Core.Models;
using TradingSoftware.Core.Services;

namespace WebApiTradingServer.ODataControllers
{
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
            return sharesManager.GetAllShares().AsQueryable();
        }

        [EnableQuery]
        public SingleResult<Share> Get([FromODataUri] int key)
        {
            IQueryable<Share> result = sharesManager.GetAllShares().Where(s => s.ShareID == key).AsQueryable();
            return SingleResult.Create<Share>(result);
        }
    }
}