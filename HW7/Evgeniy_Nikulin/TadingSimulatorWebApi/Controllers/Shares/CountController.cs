namespace TadingSimulatorWebApi.Controllers.Shares
{
    using Microsoft.AspNetCore.Mvc;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("share/[controller]")]
    public class CountController : ControllerBase
    {
        private readonly IShareService shareService;
        public CountController(IShareService shareService) =>
            this.shareService = shareService;

        // GET: /share/count?OwnerId=
        [HttpGet]
        public string Get(int OwnerId)
        {
            return shareService.GetSharesCount(OwnerId).ToString();
        }
    }
}