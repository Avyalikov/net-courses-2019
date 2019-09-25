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

namespace HW7.Server.ODataControllers
{
    [Route("odata/balances")]
    [ApiController]
    public class BalancesController : ODataController
    {
        private readonly IContextProvider contextProvider;

        public BalancesController(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        //Get traders' balances along with status
        [EnableQuery]
        public IEnumerable<BalanceWithStatus> Get()
        {
            return contextProvider.Traders.Select(x => new BalanceWithStatus
            { TraderId = x.TraderId, Balance = x.Balance, Status = x.Balance > 0 ? "green" : x.Balance == 0 ? "orange" : "black" }).ToList();
        }
    }
}