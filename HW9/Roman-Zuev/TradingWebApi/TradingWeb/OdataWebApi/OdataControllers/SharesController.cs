namespace OdataWebApi.OdataControllers
{
    using Microsoft.AspNet.OData;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Trading.Core.Models;
    using Trading.Repository.Context;

    public class SharesController : ODataController
    {
        TradesEmulatorDbContext db = new TradesEmulatorDbContext();

        [EnableQuery]
        public IQueryable<SharesEntity> Get()
        {
            return db.Shares;
        }

        public async Task<IHttpActionResult> Post([FromBody] SharesEntity share)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Shares.Add(share);
            await db.SaveChangesAsync();
            return Created(share);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}