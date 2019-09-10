namespace Traiding.WebAPIConsole.Controllers
{
    using StructureMap;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Traiding.Core.Dto;
    using Traiding.Core.Models;
    using Traiding.Core.Services;

    public class SharesController : ApiController
    {
        private readonly SharesService sharesService;
        private readonly ShareTypesService shareTypesService;
        private readonly ReportsService reportsService;

        public SharesController(SharesService sharesService, ShareTypesService shareTypesService, ReportsService reportsService)
        {
            this.sharesService = sharesService;
            this.shareTypesService = shareTypesService;
            this.reportsService = reportsService;
        }

        //public SharesController()
        //{
        //    this.sharesService = new Container(new Models.DependencyInjection.TraidingRegistry()).GetInstance<SharesService>();
        //    this.shareTypesService = new Container(new Models.DependencyInjection.TraidingRegistry()).GetInstance<ShareTypesService>();
        //    this.reportsService = new Container(new Models.DependencyInjection.TraidingRegistry()).GetInstance<ReportsService>();
        //}

        // GET /shares?clientId=...  returns all shares for client
        public IEnumerable<SharesNumberEntity> Get([FromUri]int clientId)
        {
            var clients = this.reportsService.GetSharesNumberByClient(clientId);           

            return clients;
        }

        // POST shares/add
        public HttpResponseMessage Add([FromBody]ShareInputData value)
        {
            if (value == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var shareId = sharesService.RegisterNewShare(new ShareRegistrationInfo()
            {
                CompanyName = value.CompanyName,
                Type = shareTypesService.GetShareType(value.ShareTypeId)
            });

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // POST shares/update
        public HttpResponseMessage Update([FromBody]ShareInputData value)
        {
            if (value == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            sharesService.ChangeCompanyName(value.Id, value.CompanyName);
            sharesService.ChangeType(value.Id, shareTypesService.GetShareType(value.ShareTypeId));

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // POST shares/remove
        public HttpResponseMessage Remove([FromBody]int value)
        {
            if (value == 0)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            sharesService.RemoveShare(value);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public class ShareInputData
        {
            public int Id { get; set; }
            public string CompanyName { get; set; }
            public int ShareTypeId { get; set; }
        }
    }
}
