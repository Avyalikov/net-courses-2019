using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HW7.Core;
using HW7.Core.Dto;
using HW7.Core.Models;
using HW7.Core.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Server.ODataControllers
{
    public class SharesController : ODataController
    {
        private readonly IContextProvider contextProvider;
        private readonly SharesService sharesService;

        public SharesController(IContextProvider contextProvider, SharesService sharesService)
        {
            this.contextProvider = contextProvider;
            this.sharesService = sharesService;
        }

        //Get all shares
        [EnableQuery]
        [Route("odata/shares")]
        public IEnumerable<Share> Get()
        {
            return contextProvider.Shares;
        }

        // Adds share
        [Route("odata/shares/add")]
        public async Task<ActionResult<string>> Post([FromBody]ShareToAdd share)
        {
            Share newShare = null;

            if (share == null || share.Price == 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            newShare = contextProvider.Shares.Add
            (new Share() { Name = share.Name, Price = share.Price });

            if (newShare != null)
            {
                try
                {
                    contextProvider.SaveChanges();
                }
                catch (Exception)
                {
                    return BadRequest();
                }

                return "Share added";
            }
            else
            {
                return BadRequest();
            }
        }

        //Update share
        [Route("odata/shares/update")]
        public async Task<ActionResult<string>> Put([FromBody]ShareToUpdate share)
        {
            Share shareToUpdate = null;

            try
            {
                shareToUpdate = contextProvider.Shares.Find(share.ShareId);
            }
            catch
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (shareToUpdate != null || share != null || share.Price != 0)
            {
                shareToUpdate.Name = share.Name;
                shareToUpdate.Price = share.Price;   
            }
            else
            {
                return BadRequest();
            }

            try
            {
                contextProvider.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return "Share updated";
        }

        //Remove share
        [Route("odata/shares/remove")]
        public async Task<ActionResult<string>> Delete([FromBody] ShareToRemove share)
        {
            Share shareToDelete = null;

            try
            {
                shareToDelete = contextProvider.Shares.Find(share.ShareId);
            }
            catch
            {
                return NotFound();
            }            

            if (shareToDelete == null)
            {
                return NotFound();
            }

            contextProvider.Shares.Remove(shareToDelete);

            try
            {
                contextProvider.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return "Share deleted";
        }
    }
}