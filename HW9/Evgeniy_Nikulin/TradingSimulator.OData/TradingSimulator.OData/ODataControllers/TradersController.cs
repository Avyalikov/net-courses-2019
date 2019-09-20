namespace TradingSimulator.OData.Controllers
{
    using Microsoft.AspNet.OData;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using TradingSimulator.DataBase;
    using TradingSimulator.DataBase.Model;

    public class TradersController : ODataController
    {
        TradingDbContext db = new TradingDbContext();

        [EnableQuery(PageSize = 2)]
        public IQueryable<TraderEntity> Get()
        {
            return db.Traders;
        }

        public async Task<IHttpActionResult> Post(TraderEntity trader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Traders.Add(trader);
            await db.SaveChangesAsync();
            return Created(trader);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}