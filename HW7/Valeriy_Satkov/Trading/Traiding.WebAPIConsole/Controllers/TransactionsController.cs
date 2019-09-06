namespace Traiding.WebAPIConsole.Controllers
{
    using StructureMap;
    using System.Collections.Generic;
    using System.Web.Http;
    using Traiding.Core.Models;
    using Traiding.Core.Services;

    public class TransactionsController : ApiController
    {
        private readonly ReportsService reportsService;

        public TransactionsController(ReportsService reportsService)
        {
            this.reportsService = reportsService;
        }

        //public TransactionsController()
        //{
        //    this.reportsService = new Container(new Models.DependencyInjection.TraidingRegistry()).GetInstance<ReportsService>();
        //}

        // GET /transactions?clientId=2&top=1
        public IEnumerable<OperationEntity> Get([FromUri]int clientId, int top)
        {
            var operations = this.reportsService.GetOperationByClient(clientId, top);           

            return operations;
        }
    }
}
