namespace WebApiTradingServer.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using TradingSoftware.Core.Dto;
    using TradingSoftware.Core.Services;

    [Route("[controller]")]
    [ApiController]
    public class BalancesController : ControllerBase
    {
        private readonly IClientManager clientManager;

        public BalancesController(IClientManager clientManager)
        {
            this.clientManager = clientManager;
        }

        // GET /balances?clientID=...
        [HttpGet]
        public ActionResult<IEnumerable<ClientBalanceStatus>> Get(int clientID)
        {
            return Ok(clientManager.GetClientBalanceStatus(clientID));
        }
    }
}