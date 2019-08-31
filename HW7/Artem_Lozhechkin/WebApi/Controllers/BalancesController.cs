using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Core.Services;

namespace WebApi.Controllers
{
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
