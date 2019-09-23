using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.Core.Services;

namespace TradeSimulator.Server.Controllers
{
    [Route("transaction")]
    [ApiController]
    public class TransactionHistoryController
    {
        private readonly ShowDbInfoService showDbInfoService;
        public TransactionHistoryController(ShowDbInfoService showDbInfoService)
        {
            this.showDbInfoService = showDbInfoService;
        }

        [HttpGet]
        public ActionResult<string> GetNClientsHistoryRecords(int clientId, int top)
        {
            try
            {
                var responseContent = JsonConvert.SerializeObject(this.showDbInfoService.GetNClientsHistoryRecords(clientId, top));
                return responseContent;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
