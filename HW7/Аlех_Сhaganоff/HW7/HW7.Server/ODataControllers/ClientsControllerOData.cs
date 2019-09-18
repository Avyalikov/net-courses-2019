using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HW7.Core;
using HW7.Core.Dto;
using HW7.Core.Models;
using HW7.Core.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Server.Controllers
{ 
    public class ClinetsControllerOData : ODataController
    {
        private readonly TradersService tradersService;

        public ClinetsControllerOData(TradersService tradersService)
        {
            this.tradersService = tradersService;
        }

        //Returns top 10 clients
        [Route("odata/clients")]
        public async Task<ActionResult<IEnumerable<Trader>>> Get(int top, int page)
        {
            int amounttoSkip = top * page - top;
            int amounttoTake = top;

            return await tradersService.GetListOfSeveralTraders(amounttoSkip, amounttoTake).ToListAsync();
        }

        [Route("odata/clients/sellerslist")]
        public async Task<ActionResult<IEnumerable<int>>> SellersList()
        {
            return  tradersService.GetAvailableSellers();
        }

        [Route("odata/clients/buyerslist")]
        public async Task<ActionResult<IEnumerable<int>>> BuyersList()
        {
            return tradersService.GetAvailableBuyers();
        }

        // Adds trader
        [Route("odata/clients/add")]
        public async Task<ActionResult<string>> Add([FromBody]TraderToAdd trader)
        {
            var newTrader = tradersService.AddTrader(trader);

            if (newTrader != null)
            {
                return new ActionResult<string>("New record added");
            }
            else
            {
                //return BadRequest();
                return new ActionResult<string>("Can't add record");
            }
        }

        //Update trader
        [Route("odata/clients/update")]
        public async Task<ActionResult<string>> Update([FromBody]TraderToUpdate trader)
        {
            var updatedTrader = tradersService.UpdateTrader(trader);

            if (updatedTrader == null)
            {
                //return BadRequest();
                return new ActionResult<string>("Can't update record");
            }

            return new ActionResult<string>("Record updated");
        }

        //Remove trader
        [Route("odata/clients/remove")]
        public async Task<ActionResult<string>> Remove([FromBody]TraderToRemove trader)
        {
            var isDeleted = tradersService.RemoveTrader(trader.TraderId);

            if (isDeleted == false)
            {
                return new ActionResult<string>("Record not found");
            }

            return new ActionResult<string>("Record deleted");
        }
    }
}