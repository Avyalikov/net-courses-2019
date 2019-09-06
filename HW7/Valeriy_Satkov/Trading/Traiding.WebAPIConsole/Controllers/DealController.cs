namespace Traiding.WebAPIConsole.Controllers
{
    using StructureMap;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Traiding.Core.Services;

    public class DealController : ApiController
    {
        private readonly SalesService salesService;

        public DealController(SalesService salesService)
        {
            this.salesService = salesService;
        }

        //public DealController()
        //{
        //    this.salesService = new Container(new Models.DependencyInjection.TraidingRegistry()).GetInstance<SalesService>();
        //}

        // POST deal/make
        public IHttpActionResult Make([FromBody]OperationInputData value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            try
            {
                salesService.Deal(
                customerId: value.CustomerId,
                sellerId: value.SellerId,
                shareId: value.ShareId,
                requiredSharesNumber: value.RequiredSharesNumber);                
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }

            return Ok();
        }

        public class OperationInputData
        {
            public int CustomerId { get; set; }
            public int SellerId { get; set; }
            public int ShareId { get; set; }
            public int RequiredSharesNumber { get; set; }
        }
    }
}
