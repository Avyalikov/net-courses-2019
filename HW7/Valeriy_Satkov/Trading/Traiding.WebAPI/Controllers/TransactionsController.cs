namespace Traiding.WebAPI.Controllers
{
    using StructureMap;
    using System.Collections.Generic;
    using System.Web.Http;
    using Traiding.Core.Models;
    using Traiding.Core.Services;

    public class TransactionsController : ApiController
    {
        private readonly ReportsService reportsService;

        public TransactionsController()
        {
            this.reportsService = new Container(new Models.DependencyInjection.TraidingRegistry()).GetInstance<ReportsService>();
        }

        // GET /clients?top=10&page=1  return first 10 clients
        public IEnumerable<OperationEntity> Get([FromUri]int clientId, int top)
        {
            var clients = this.reportsService.GetOperationByClient(clientId, top);           

            return clients;
        }
    }
}
