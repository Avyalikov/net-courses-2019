using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HW7.Core;
using HW7.Core.Dto;
using HW7.Core.Models;
using HW7.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Server.Controllers
{
    public class SharesController : ControllerBase
    {
        private readonly SharesService sharesService;

        public SharesController(SharesService sharesService)
        {
            this.sharesService = sharesService;
        }

        //Returns client's portfolio with share price
        [Route("shares")]
        [HttpGet]
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

        [Route("shares/availableshares")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<int>>> GetAvailableShares(int clientId)
        {
            return sharesService.GetAvailableShares(clientId);
        }

        //Adds share
        [Route("shares/add")]
        [HttpPost]
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
        [Route("shares/update")]
        [HttpPost]
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
        [Route("shares/remove")]
        [HttpPost]
        public async Task<ActionResult<string>> Remove([FromBody]ShareToRemove share)
        {
            bool isDeleted = false;

            if (share != null)
            {
                isDeleted = sharesService.RemoveShare(share.ShareId);
            }

            if (isDeleted == false)
            {
                return new ActionResult<string>("Record not found");
            }

            return new ActionResult<string>("Record deleted");
        }
    }
}