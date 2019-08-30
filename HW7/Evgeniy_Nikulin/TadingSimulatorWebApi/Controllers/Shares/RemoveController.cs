namespace TadingSimulatorWebApi.Controllers.Shares
{
    using Microsoft.AspNetCore.Mvc;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("share/[controller]")]
    public class RemoveController : ControllerBase
    {
        private readonly IShareService shareService;
        public RemoveController(IShareService shareService) =>
            this.shareService = shareService;

        // POST: share/<controller>/
        [HttpPost]
        public void Post(int Id)
        {
            shareService.Remove(Id);
        }
    }
}