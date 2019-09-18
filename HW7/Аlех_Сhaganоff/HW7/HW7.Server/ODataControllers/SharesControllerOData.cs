using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HW7.Core;
using HW7.Core.Dto;
using HW7.Core.Models;
using HW7.Core.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Server.Controllers
{
    public class SharesControllerOData : ODataController
    {
        private readonly SharesService sharesService;

        public SharesControllerOData(SharesService sharesService)
        {
            this.sharesService = sharesService;
        }

        //Returns client's portfolio with share price
        [Route("odata/shares")]
        public async Task<ActionResult<IEnumerable<ShareWithPrice>>> Get(int clientId)
        {
            var result = sharesService.GetSharesWithPrice(clientId);

            if(result != null)
            {
                return await result.ToListAsync();
            }
            else
            {
                return NotFound();
            }
        }

        [Route("odata/shares/availableshares")]
        public async Task<ActionResult<IEnumerable<int>>> GetAvailableShares(int clientId)
        {
            return sharesService.GetAvailableShares(clientId);
        }

        //Adds share
        [Route("odata/shares/add")]
        public async Task<ActionResult<string>> Add([FromBody]ShareToAdd share)
        {
            var newShare = sharesService.AddShare(share);

            if (newShare != null)
            {
                return new ActionResult<string>("New record added");
            }
            else
            {
                return new ActionResult<string>("Can't add record");
            }
        }

        //Update share
        [Route("odata/shares/update")]
        public async Task<ActionResult<string>> Update([FromBody]ShareToUpdate share)
        {
            var updatedShare = sharesService.UpdateShare(share);

            if (updatedShare == null)
            {
                return new ActionResult<string>("Can't update record");
            }

            return new ActionResult<string>("Record updated");
        }

        //Remove share
        [Route("odata/shares/remove")]
        public async Task<ActionResult<string>> Remove([FromBody]ShareToRemove share)
        {
            var isDeleted = sharesService.RemoveShare(share.ShareId);

            if (isDeleted == false)
            {
                return new ActionResult<string>("Record not found");
            }

            return new ActionResult<string>("Record deleted");
        }
    }
}