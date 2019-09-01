namespace WebApiTradingServer.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Services;

    [Route("shares")]
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
        public ActionResult<IEnumerable<string>> GetClientShares(int clientID)
        {
            return Ok(blockOfSharesManager.GetClientShares(clientID));
        }

        // POST shares/add
        [HttpPost]
        [Route("add")]
        public ActionResult<string> AddClientShares([FromBody] BlockOfShares blockOfShare)
        {
            blockOfSharesManager.AddShare(blockOfShare);
            return Ok($"Share ID {blockOfShare.ShareID} added to client ID {blockOfShare.ClientID}");
        }

        // POST shares/remove
        [HttpPost]
        [Route("update")]
        public ActionResult<string> UpdateClientShares([FromBody] BlockOfShares blockOfShare)
        {
            blockOfSharesManager.UpdateClientShares(blockOfShare);
            return Ok($"BlockOfShares updated");
        }

        // POST shares/remove
        [HttpPost]
        [Route("remove")]
        public ActionResult<string> DeleteClientShares([FromBody] BlockOfShares blockOfShare)
        {
            this.blockOfSharesManager.Delete(blockOfShare);
            return Ok($"Share ID {blockOfShare.ShareID} deleted from client ID {blockOfShare.ClientID}");
        }
    }
}