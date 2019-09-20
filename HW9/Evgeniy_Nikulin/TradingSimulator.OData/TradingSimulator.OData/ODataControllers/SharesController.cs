namespace TradingSimulator.OData.Controllers
{
    using Microsoft.AspNet.OData;
    using System.Linq;
    using TradingSimulator.DataBase;
    using TradingSimulator.DataBase.Model;

    public class SharesController : ODataController
    {
        TradingDbContext db = new TradingDbContext();

        [EnableQuery]
        public IQueryable<ShareEntity> Get()
        {
            return db.Shares;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}