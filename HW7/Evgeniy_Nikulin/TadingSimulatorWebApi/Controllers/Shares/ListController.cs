namespace TadingSimulatorWebApi.Controllers.Shares
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("share/[controller]")]
    public class ListController : ControllerBase
    {
        private readonly IShareService shareService;
        public ListController(IShareService shareService) =>
            this.shareService = shareService;

        // GET: /share/list?ownerId=
        [HttpGet]
        public string Get(string ownerId)
        {
            return JsonConvert.SerializeObject(shareService.GetShareList(ownerId));
        }
    }
}