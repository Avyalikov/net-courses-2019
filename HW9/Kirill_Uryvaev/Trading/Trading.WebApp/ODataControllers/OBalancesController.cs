using Microsoft.AspNet.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Trading.Core;
using Trading.Core.Services;

namespace Trading.WebApp.ODataControllers
{
    public class OBalancesController : ODataController
    {
        private readonly BalanceService balanceService;
        public OBalancesController(BalanceService balanceService)
        {
            this.balanceService = balanceService;
        }

        [EnableQuery]
        public IQueryable<BalanceEntity> Get()
        {
            return balanceService.GetAllBalances();
        }
    }
}
