using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TradingSoftware.Core.Models;
using TradingSoftware.Core.Services;

namespace WebApiTradingServer.Controllers
{
    [Route("[controller]")]
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
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] ValuesRequestData value)
        {
            return new ActionResult<string>("testResponse");
        }
    }


    [Route("shares/[controller]")]
    [ApiController]
    public class AddController : ControllerBase
    {
        private readonly IBlockOfSharesManager blockOfSharesManager;

        public AddController(IBlockOfSharesManager blockOfSharesManager)
        {
            this.blockOfSharesManager = blockOfSharesManager;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] BlockOfShares blockOfShare)
        {
            blockOfSharesManager.AddShare(blockOfShare);
            return new ActionResult<string>("Success");
        }
    }

    [Route("shares/[controller]")]
    [ApiController]
    public class RemoveController : ControllerBase
    {
        private readonly IBlockOfSharesManager blockOfSharesManager;

        public RemoveController(IBlockOfSharesManager blockOfSharesManager)
        {
            this.blockOfSharesManager = blockOfSharesManager;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] BlockOfShares blockOfShare)
        {
            blockOfSharesManager.Delete(blockOfShare);
            return new ActionResult<string>("Success");
        }
    }


    [Route("shares/[controller]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private readonly IBlockOfSharesManager blockOfSharesManager;

        public UpdateController(IBlockOfSharesManager blockOfSharesManager)
        {
            this.blockOfSharesManager = blockOfSharesManager;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] BlockOfShares blockOfShare)
        {
            blockOfSharesManager.UpdateClientShares(blockOfShare);
            return new ActionResult<string>("Success");
        }
    }
}