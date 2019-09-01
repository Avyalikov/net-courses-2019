using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using TradingSimulator.Core.Interfaces;
using TradingSimulator.Core.Services;

namespace WebApiServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService historyService;

        public HistoryController(IHistoryService historyService)
        {
            this.historyService = historyService;
        }

        // GET /history?clientId=_&maxCount=_
        [HttpGet]
        public string Get(int clientId, int maxCount)
        {
            return JsonConvert.SerializeObject(historyService.GetHistoryById(clientId, maxCount));
        }
    }
}