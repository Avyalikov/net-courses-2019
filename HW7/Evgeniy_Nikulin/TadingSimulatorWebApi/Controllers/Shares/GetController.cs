namespace TadingSimulatorWebApi.Controllers.Shares
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("share/[controller]")]
    public class GetController : ControllerBase
    {
        private readonly IShareService shareService;
        public GetController(IShareService shareService)=>
            this.shareService = shareService;

        // GET: /share/get?OwnerId=_&Index=_
        [HttpGet]
        public string Get(int OwnerId, int Index)
        {
            return JsonConvert.SerializeObject(shareService.GetShareByIndex(OwnerId, Index));
        }
    }
}