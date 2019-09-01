namespace TadingSimulatorWebApi.Controllers.Shares
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("[controller]")]
    public class ShareController : ControllerBase
    {
        private readonly IShareService shareService;
        public ShareController(IShareService shareService)=>
            this.shareService = shareService;

        // GET: /share/get?OwnerId=_&Index=_
        [HttpGet("get")]
        public string Get(int OwnerId, int Index)
        {
            return JsonConvert.SerializeObject(shareService.GetShareByIndex(OwnerId, Index));
        }

        // GET: /share/count?OwnerId=
        [HttpGet("count")]
        public string Get(int OwnerId)
        {
            return shareService.GetSharesCount(OwnerId).ToString();
        }

        // GET: /share/list?ownerId=
        [HttpGet("list")]
        public string Get(string ownerId)
        {
            return JsonConvert.SerializeObject(shareService.GetShareList(ownerId));
        }

        // POST: /share/add?shareName=_&price=_&quantity=_&ownerId=_
        [HttpPost("add")]
        public string PostAdd(string shareName, string price, string quantity, string ownerId)
        {
            return shareService.AddShare(shareName, price, quantity, ownerId);
        }


        // POST: /share/remove?id=_
        [HttpPost("remove")]
        public void PostRemove(int Id)
        {
            shareService.Remove(Id);
        }

        // POST: share/update?shareId=_&newName=_&newPrice=_&ownerId=_
        [HttpPost("update")]
        public string PostUpdate(string shareId, string newName, string newPrice, string ownerId)
        {
            return shareService.ChangeShare(shareId, newName, newPrice, ownerId);
        }
    }
}