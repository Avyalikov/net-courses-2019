namespace TadingSimulatorWebApi.Controllers.Shares
{
    using Microsoft.AspNetCore.Mvc;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("share/[controller]")]
    public class AddController : ControllerBase
    {

        private readonly IShareService shareService;
        public AddController(IShareService shareService) =>
            this.shareService = shareService;

        // POST: share/add?shareName=_&price=_&quantity=_&ownerId=_
        [HttpPost]
        public string Post(string shareName, string price, string quantity, string ownerId)
        {
            return shareService.AddShare(shareName, price, quantity, ownerId);
        }
    }
}