namespace TradingApp.OwinHostApi.Controller
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Web.Http;
    using TradingApp.Core.Dto;
    using TradingApp.Core.ServicesInterfaces;

    public class SharesController : ApiController
    {
        private readonly IShareServices shareServices;

        public SharesController(IShareServices shareServices)
        {
            this.shareServices = shareServices;
        }

        public IHttpActionResult GetUsersShares(int clientid)
        {
            return Json(shareServices.GetUsersShares(clientid));
        }

        [ActionName("update")]
        public void PutUpdateShare(int id, JObject json)
        {
            var share = JsonConvert.DeserializeObject<ShareInfo>(json.ToString());
            shareServices.Update(id, share);
        }

        [ActionName("add")]
        public void PostAdd(JObject json)
        {
            var share = JsonConvert.DeserializeObject<ShareInfo>(json.ToString());
            shareServices.AddNewShare(share);
        }

        [ActionName("remove")]
        public void DeleteShare(int id)
        {
            shareServices.Remove(id);
        }
    }
}
