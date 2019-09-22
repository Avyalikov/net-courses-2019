using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HW7.Core;
using HW7.Core.Dto;
using HW7.Core.Models;
using HW7.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Server.Controllers
{ 
    public class ClinetsController : ControllerBase
    {
        private readonly TradersService tradersService;

        public ClinetsController(TradersService tradersService)
        {
            this.tradersService = tradersService;
        }

        //Returns top 10 clients
        [Route("clients")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trader>>> Get(int top, int page)
        {
            int amounttoSkip = top * page - top;
            int amounttoTake = top;

            return await tradersService.GetListOfSeveralTraders(amounttoSkip, amounttoTake).ToListAsync();
        }

        [Route("clients/sellerslist")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<int>>> SellersList()
        {
            return  tradersService.GetAvailableSellers();
        }

        [Route("clients/buyerslist")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<int>>> BuyersList()
        {
            return tradersService.GetAvailableBuyers();
        }

        // Adds trader
        [Route("clients/add")]
        [HttpPost]
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
        [Route("clients/update")]
        [HttpPost]
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
        [Route("clients/remove")]
        [HttpPost]
        public async Task<ActionResult<string>> Remove([FromBody]TraderToRemove trader)
        {
            bool isDeleted = false;

            if (trader != null)
            {
                isDeleted = tradersService.RemoveTrader(trader.TraderId);
            }

            if (isDeleted == false)
            {
                return new ActionResult<string>("Record not found");
            }

            return new ActionResult<string>("Record deleted");
        }
    }
}