namespace Traiding.WebAPIConsole.Controllers
{
    using StructureMap;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Traiding.Core.Dto;
    using Traiding.Core.Models;
    using Traiding.Core.Services;

    public class ClientsController : ApiController
    {
        private readonly ClientsService clientsService;
        private readonly BalancesService balancesService;
        private readonly SalesService salesService;
        private readonly ReportsService reportsService;

        public ClientsController(ClientsService clientsService, BalancesService balancesService, SalesService salesService, ReportsService reportsService)
        {
            this.clientsService = clientsService;
            this.balancesService = balancesService;
            this.salesService = salesService;
            this.reportsService = reportsService;
        }

        //public ClientsController()
        //{
        //    this.clientsService = new Container(new Models.DependencyInjection.TraidingRegistry()).GetInstance<ClientsService>();
        //    this.balancesService = new Container(new Models.DependencyInjection.TraidingRegistry()).GetInstance<BalancesService>();
        //    this.salesService = new Container(new Models.DependencyInjection.TraidingRegistry()).GetInstance<SalesService>();
        //    this.reportsService = new Container(new Models.DependencyInjection.TraidingRegistry()).GetInstance<ReportsService>();
        //}

        // GET /clients?top=10&page=1  return first 10 clients
        public IEnumerable<ClientEntity> Get([FromUri]int top, int page)
        {
            var clients = this.reportsService.GetFirstClients(top, page);
            
            return clients;
        }

        // POST clients/add
        public HttpResponseMessage Add([FromBody]ClientInputData value)
        {
            if (value == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var clientId = clientsService.RegisterNewClient(new ClientRegistrationInfo()
            {
                LastName = value.LastName,
                FirstName = value.FirstName,
                PhoneNumber = value.PhoneNumber
            });

            balancesService.RegisterNewBalance(new BalanceRegistrationInfo()
            {
                Client = clientsService.GetClient(clientId),
                Amount = value.Amount
            });

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // POST clients/update
        public HttpResponseMessage Update([FromBody]ClientInputData value)
        {
            if (value == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }           

            clientsService.UpdateClientData(value.Id, new ClientRegistrationInfo()
            {
                LastName = value.LastName,
                FirstName = value.FirstName,
                PhoneNumber = value.PhoneNumber
            });

            //// Uncomment if it will need
            //var balance = salesService.SearchBalanceByClientId(value.Id);
            //balancesService.ChangeBalance(balance.Id, value.Amount);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // POST clients/remove
        public HttpResponseMessage Remove([FromBody]int value)
        {
            if (value == 0)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            clientsService.RemoveClient(value);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public class ClientInputData
        {
            public int Id { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string PhoneNumber { get; set; }
            public decimal Amount { get; set; }
        }
    }
}
