using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.Core.Dto;
using TradeSimulator.Core.Services;

namespace TradeSimulator.Server.Controllers
{
    [Route("shares")]
    [ApiController]

    public class SharesController
    {
        private readonly ClientsService clientsService;
        private readonly ShowDbInfoService showDbInfoService;

        public SharesController(ClientsService clientsService, ShowDbInfoService showDbInfoService)
        {
            this.clientsService = clientsService;
            this.showDbInfoService = showDbInfoService;
        }

        [HttpGet]
        public ActionResult<string> GetStocksOfClient(int clientId)
        {
            try
            {
                var responseContent = JsonConvert.SerializeObject(this.showDbInfoService.GetAllStocksOfClientWithPriceByClientId(clientId));
                return responseContent;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [Route("add")]
        [HttpPost]
        public ActionResult<HttpResponseMessage> AddStock([FromBody] StockOfClientInfo stockOfClientInfo)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                this.clientsService.RegisterStockForNewClient(stockOfClientInfo);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            catch (Exception)
            {
                response.StatusCode = System.Net.HttpStatusCode.Conflict;
                return response;
            }
        }
        [Route("update")]
        [HttpPost]
        public ActionResult<HttpResponseMessage> UpdateStock([FromBody] StockOfClientInfo stockOfClientInfo)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                this.clientsService.UpdateStock(stockOfClientInfo);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            catch (Exception)
            {
                response.StatusCode = System.Net.HttpStatusCode.Conflict;
                return response;
            }
        }
        [Route("remove")]
        [HttpPost]
        public ActionResult<HttpResponseMessage> RemoveStock([FromBody] StockOfClientInfo stockOfClientInfo)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                this.clientsService.RemoveStock(stockOfClientInfo);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            catch (Exception)
            {
                response.StatusCode = System.Net.HttpStatusCode.Conflict;
                return response;
            }
        }

        [Route("all")]
        [HttpGet]
        public ActionResult<string> GetAllStocksOfClient()
        {
            try
            {
                var responseContent = JsonConvert.SerializeObject(this.showDbInfoService.GetAllStocksOfClient());
                return responseContent;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [Route("check")]
        [HttpGet]
        public ActionResult<string> GetCheckResultIfStockPriceAlreadyExists(string type)
        {
            try
            {
                var responseContent = JsonConvert.SerializeObject(this.clientsService.CheckIfStockPriseConteinStockOfClientByTypeOfStock(type));
                return responseContent;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
