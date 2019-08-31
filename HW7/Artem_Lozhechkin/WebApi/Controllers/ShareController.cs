using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TradingApp.Core.DTO;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;
using TradingApp.Core.Services;

namespace WebApi.Controllers
{
    [Route("shares")]
    [ApiController]
    public class ShareController : ControllerBase
    {
        private readonly ShareService shareService;

        public ShareController(ShareService shareService)
        {
            this.shareService = shareService;
        }

        [HttpGet]
        public ActionResult<List<string>> GetSharesByClientId(int clientId)
        {
            try
            {
                return Ok(this.shareService.GetAllSharesListByTraderId(clientId));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("add")]
        [HttpPost]
        public ActionResult AddShare([FromBody] ShareInfo shareInfo)
        {
            try
            {
                this.shareService.AddNewShare(shareInfo);
                return Ok("Share added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("update")]
        [HttpPost]
        public ActionResult UpdateShare([FromBody] ShareInfo shareInfo)
        {
            try
            {
                this.shareService.UpdateShare(shareInfo);
                return Ok("Share updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("remove")]
        [HttpPost]
        public ActionResult RemoveShare([FromBody] ShareInfo shareInfo)
        {
            try
            {
                this.shareService.RemoveShare(shareInfo.Id);
                return Ok("Share removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
