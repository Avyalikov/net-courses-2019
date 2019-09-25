using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HW7.Core;
using HW7.Core.Dto;
using HW7.Core.Models;
using HW7.Core.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Server.ODataControllers
{
    
    public class ClinetsController : ODataController
    {
        private readonly IContextProvider contextProvider;
        private readonly TradersService tradersService;

        public ClinetsController(IContextProvider contextProvider, TradersService tradersService)
        {
            this.contextProvider = contextProvider;
            this.tradersService = tradersService;
        }

        //Get all traders
        [EnableQuery]
        [Route("odata/clients")]
        public IEnumerable<Trader> Get()
        {
            return contextProvider.Traders;
        }

        //Get portfolios of all traders
        [EnableQuery]
        [Route("odata/clients/portfolios")]
        public IEnumerable<Portfolio> GetPortfolios()
        {
            return contextProvider.Portfolios;
        }

        //Get portfolios of all traders with share prices
        [EnableQuery]
        [Route("odata/clients/portfolioswithprices")]
        public IEnumerable<ShareWithPrice> GetPortfoliosWithPrices()
        {
            var result = from p in contextProvider.Portfolios
            join s in contextProvider.Shares on p.ShareId equals s.ShareId
            select new ShareWithPrice { TraderId = p.TraderID, ShareId = p.ShareId, Quantity = p.Quantity, Price = s.Price };

            return result.ToList();
        }
        
        // Adds trader
        [Route("odata/clients/add")]
        public async Task<ActionResult<string>> Post([FromBody]TraderToAdd trader)
        {
            Trader newTrader = null;

            if (trader == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
    
            newTrader = contextProvider.Traders.Add
            (new Trader() { FirstName = trader.FirstName, LastName = trader.LastName, Balance = trader.Balance, PhoneNumber = trader.PhoneNumber });
            
            if (newTrader != null)
            {
                try
                {
                    contextProvider.SaveChanges();
                }
                catch (Exception)
                {
                    return BadRequest();
                }

                return "New trader added";
            }
            else
            {
                return BadRequest();
            }
        }

        //Update trader
        [Route("odata/clients/update")]
        public async Task<ActionResult<string>> Put([FromBody]TraderToUpdate trader)
        {
            Trader traderToUpdate = null;

            try
            {
                traderToUpdate = contextProvider.Traders.Find(trader.TraderId);
            }
            catch
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (traderToUpdate != null || trader != null)
            {
                traderToUpdate.FirstName = trader.FirstName;
                traderToUpdate.LastName = trader.LastName;
                traderToUpdate.PhoneNumber = trader.PhoneNumber;
            }
            else
            {
                return BadRequest();
            }
             
            try
            {
                contextProvider.SaveChanges();
            }
            catch (Exception)
            {
               return BadRequest();
            }

            return "Trader updated";
        }

        //Remove trader
        [Route("odata/clients/remove")]
        public async Task<ActionResult<string>> Delete([FromBody]TraderToRemove trader)
        {
            Trader traderToDelete = null;

            try
            {
                traderToDelete = contextProvider.Traders.Find(trader.TraderId);
            }
            catch
            {
                return NotFound();
            }
            
            if (traderToDelete == null)
            {
                return NotFound();
            }

            contextProvider.Traders.Remove(traderToDelete);

            try
            {
                contextProvider.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return "Trader deleted";
        }
    }
}