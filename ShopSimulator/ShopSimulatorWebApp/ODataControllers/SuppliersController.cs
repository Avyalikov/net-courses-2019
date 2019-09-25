using Microsoft.AspNet.OData;
using ShopSimulator.ConsoleApp;
using ShopSimulator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ShopSimulatorWebApp.ODataControllers
{
    public class SuppliersController : ODataController
    {
        ShopSimulatorDbContext db = new ShopSimulatorDbContext("shopSimulatorConnectionString");
        public SuppliersController()
        {

        }

        [EnableQuery]
        public IQueryable<SupplierEntity> Get()
        {
            return db.Suppliers;
        }

        public async Task<IHttpActionResult> Post(SupplierEntity supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Suppliers.Add(supplier);
            await db.SaveChangesAsync();
            return Created(supplier);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}