using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Trading.Core;

namespace Trading.WebApp.Controllers
{
    class SharesController : ApiController
    {
        private readonly IClientsSharesService shareService;

        public SharesController(IClientsSharesService shareService)
        {
            this.shareService = shareService;
        }
    }
}
