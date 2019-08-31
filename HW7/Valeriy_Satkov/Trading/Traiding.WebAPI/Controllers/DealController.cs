namespace Traiding.WebAPI.Controllers
{
    using StructureMap;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Traiding.Core.Services;

    public class DealController : ApiController
    {
        private readonly SalesService salesService;

        public DealController()
        {
            this.salesService = new Container(new Models.DependencyInjection.TraidingRegistry()).GetInstance<SalesService>();
        }

        // POST deal/make
        public HttpResponseMessage Make([FromBody]OperationInputData value)
        {
            if (value == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            salesService.Deal(value.CustomerId, value.ShareId, value.RequiredSharesNumber);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public class OperationInputData
        {
            public int CustomerId { get; set; }
            public int ShareId { get; set; }
            public int RequiredSharesNumber { get; set; }
        }
    }
}
