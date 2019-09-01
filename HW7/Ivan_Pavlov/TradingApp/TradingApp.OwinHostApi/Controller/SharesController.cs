namespace TradingApp.OwinHostApi.Controller
{
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
            ShareInfo share = new ShareInfo()
            {
                Name = json.Value<string>("Name"),
                CompanyName = json.Value<string>("CompanyName"),
                Price = json.Value<decimal>("Price")
            };
            shareServices.Update(id, share);
        }

        [ActionName("add")]
        public void PostAdd(JObject json)
        {
            ShareInfo share = new ShareInfo()
            {
                Name = json.Value<string>("Name"),
                CompanyName = json.Value<string>("CompanyName"),
                Price = json.Value<decimal>("Price")
            };
            shareServices.AddNewShare(share);
        }

        [ActionName("remove")]
        public void DeleteShare(int id)
        {
            shareServices.Remove(id);
        }
    }
}
