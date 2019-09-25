namespace OdataWebApi.OdataControllers
{
    using Microsoft.AspNet.OData;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using Trading.Core.Models;
    using Trading.Repository.Context;

    public class ClientsController: ODataController
    {
        TradesEmulatorDbContext db = new TradesEmulatorDbContext();

        [EnableQuery(PageSize =5)]
        public IQueryable<ClientEntity> Get()
        {
            return db.Clients;
        }

        [EnableQuery]
        public async Task<IHttpActionResult> Get(int key)
        {
            var client = SingleResult.Create(db.Clients.Where(c => c.Id == key));
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        public async Task<IHttpActionResult> Post([FromBody] ClientEntity client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Clients.Add(client);
            await db.SaveChangesAsync();
            return Created(client);
        }

        public async Task<IHttpActionResult> GetName(int key)
        {
            ClientEntity client = await db.Clients.FindAsync(key);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client.Name);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}