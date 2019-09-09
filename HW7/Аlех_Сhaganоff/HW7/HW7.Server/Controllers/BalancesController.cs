using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HW7.Core;
using HW7.Core.Dto;
using HW7.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Server.Controllers
{
    [Route("balances")]
    [ApiController]
    public class BalancesController : ControllerBase
    {
        private readonly TradersService tradersService;

        public BalancesController(TradersService tradersService)
        {
            this.tradersService = tradersService;
        }

        //Returns clinet's balance along with status
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BalanceWithStatus>>> Get(int clientId)
        {
            return await tradersService.GetTraderBalanceWithStatus(clientId).ToListAsync();
        }
    }
}