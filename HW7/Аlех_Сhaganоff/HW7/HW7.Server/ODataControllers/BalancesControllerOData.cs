using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HW7.Core;
using HW7.Core.Dto;
using HW7.Core.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Server.Controllers
{
    [Route("odata/balances")]
    [ApiController]
    public class BalancesControllerOData : ODataController
    {
        private readonly TradersService tradersService;

        public BalancesControllerOData(TradersService tradersService)
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