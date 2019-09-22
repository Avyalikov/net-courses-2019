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
    [Route("balance")]
    [ApiController]
    public class BalanceController
    {
        private readonly ShowDbInfoService showDbInfoService;
        public BalanceController(ShowDbInfoService showDbInfoService)
        {
            this.showDbInfoService = showDbInfoService;
        }

        [HttpGet]
        public ActionResult<string> GetBalanceOfClient(int clientId)
        {
            try
            {
                var responseContent = JsonConvert.SerializeObject(this.showDbInfoService.GetAccountInfoByClientId(clientId));
                return responseContent;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
