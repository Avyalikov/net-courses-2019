using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TradingSoftware.Core.Models;
using TradingSoftware.Core.Services;

namespace WebApiTradingServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharesController : ControllerBase
    {
        private readonly IBlockOfSharesManager blockOfSharesManager;

        public SharesController(IBlockOfSharesManager blockOfSharesManager)
        {
            this.blockOfSharesManager = blockOfSharesManager;
        }

        // GET /shares
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get(int clientID)
        {
            return Ok(blockOfSharesManager.GetClientShares(clientID));
            /*List<string> answer = new List<string>();
            foreach (var share in clientShares)
            {
                answer.Add($"ShareID:{share.ShareID} ShareAmount: {share.Amount}");
            }
            return answer.ToArray();*/
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] ValuesRequestData value)
        {
            return new ActionResult<string>("testResponse");
        }
    }
}
