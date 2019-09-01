namespace TradingApp.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using TradingApp.Core.Services;

    [Route("balances")]
    [ApiController]
    public class BalancesController : ControllerBase
    {
        private readonly TraderService traderService;

        public BalancesController(TraderService traderService)
        {
            this.traderService = traderService;
        }
        [Route("")]
        [HttpGet]
        public ActionResult<string> GetUserStatus(int clientId)
        {
            try
            {
                return Ok(this.traderService.GetUserStatus(clientId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
