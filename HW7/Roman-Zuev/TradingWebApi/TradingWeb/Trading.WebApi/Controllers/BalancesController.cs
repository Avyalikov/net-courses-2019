using Microsoft.AspNetCore.Mvc;
using System;
using Trading.Core.Services;

namespace Trading.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BalancesController : ControllerBase
    {
        private readonly IClientsService clientsService;

        public BalancesController(IClientsService clientsService)
        {
            this.clientsService = clientsService;
        }

        [HttpGet]
        public ActionResult<string> GetBalance(int clientId)
        {
            try
            {
                return clientsService.GetBalance(clientId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
