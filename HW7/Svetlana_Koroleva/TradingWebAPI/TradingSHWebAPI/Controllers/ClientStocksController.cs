using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trading.Core.Services;
using Trading.Core.IServices;
using SharedContext;
using Trading.Core;
using Trading.Core.DTO;
using Trading.Core.Model;
using Microsoft.AspNetCore.Http;
using SharedContext.DAL;
using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TradingSHWebAPI
{
    [Route("shares")]
    [Produces("application/json")]
    [ApiController]
    public class ClientStocksController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IClientStockService clientStockService;
        private readonly IStockService stockService;

        public ClientStocksController(IUnitOfWork unitOfWork, IClientStockService service, IStockService stockService)
        {
            this.unitOfWork = unitOfWork;
            this.clientStockService = service;
            this.stockService = stockService;
        }


        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody]ClientStockInfo clientStock)
        {
            try
            {
                if (clientStock == null)
                {
                    return BadRequest();
                }

                this.clientStockService.AddClientStockToDB(clientStock);
                return Ok(clientStock);
            }
            catch (Exception e)
            {
                var ex = e.Message;
                return StatusCode(500);

            }
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update(int clientid, int stockid, [FromBody] ClientStockInfo info)
        {
            try
            {
                this.clientStockService.Update(clientid, stockid, info);
                return Ok();
            }
            catch (Exception e)
            {
                var ex = e.Message;
                return StatusCode(500);

            }
        }

        [HttpPost]
        [Route("remove")]
        public IActionResult Remove(int clientid, int stockid)
        {
            try
            {
                this.clientStockService.Delete(clientid, stockid);
                return Ok();
            }
            catch (Exception e)
            {
                var ex = e.Message;
                return StatusCode(500);

            }
        }




        [HttpGet]
        public IActionResult GetClientStockWithPrice(int clientid)
        {
            try
            {
                var clientStocks = this.clientStockService.GetClientStocksWithPrice(clientid);
                return Ok(clientStocks);
            }
            catch (Exception e)
            {
                var ex = e.Message;
                return StatusCode(500);
            }

        }



        [HttpGet]
        [Route("getclientstock")]
        public IActionResult GetClientStock(int clientid)
        {
            try
            {
                var clientStocks = this.clientStockService.GetclientStocks(clientid);
                return Ok(clientStocks);
            }
            catch (Exception e)
            {
                var ex = e.Message;
                return StatusCode(500);
            }

        }

        [HttpGet]
        [Route("getstock")]
        public IActionResult GetStockDetails(int stockid)
        {
            try
            {
                var stock=this.stockService.GetEntityByID(stockid);
                return Ok(stock);
            }
            catch (Exception e)
            {
                var ex = e.Message;
                return StatusCode(500);

            }
        }

    }
}
