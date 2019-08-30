namespace TadingSimulatorWebApi.Controllers.Shares
{
    using Microsoft.AspNetCore.Mvc;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("share/[controller]")]
    public class UpdateController : ControllerBase
    {

        private readonly IShareService shareService;
        public UpdateController(IShareService shareService) =>
            this.shareService = shareService;

        // POST: share/update?shareId=_&newName=_&newPrice=_&ownerId=_
        [HttpPost]
        public string Post(string shareId, string newName, string newPrice, string ownerId)
        {
            return shareService.ChangeShare(shareId, newName, newPrice, ownerId);
        }
    }
}