using Microsoft.AspNet.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Trading.Core;
using Trading.Core.DataTransferObjects;
using Trading.Core.Services;

namespace Trading.WebApp.ODataControllers
{
    public class OClientsController : ODataController
    {
        private readonly ClientService clientService;
        public OClientsController(ClientService clientService)
        {
            this.clientService = clientService;
        }

        [EnableQuery]
        public IQueryable<ClientEntity> Get()
        {
            return clientService.GetAllClients();
        }

        public async Task<IHttpActionResult> Post(ClientEntity client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ClientRegistrationInfo clientInfo = new ClientRegistrationInfo() { FirstName = client.ClientFirstName, LastName = client.ClientLastName, PhoneNumber = client.PhoneNumber };
            clientService.AddClient(clientInfo);
            return Created(client);
        }

    }
}
