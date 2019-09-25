namespace TradingApp.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using TradingApp.Core.DTO;
    using TradingApp.Core.Models;
    using TradingApp.Core.Services;

    [Route("shares")]
    [ApiController]
    public class ShareController : ControllerBase
    {
        private readonly ShareService shareService;

        public ShareController(ShareService shareService)
        {
            this.shareService = shareService;
        }
        [Route("client_shares")]
        [HttpGet]
        public ActionResult<string> GetSharesByClientId(int clientId)
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(this.shareService.GetAllSharesByTraderId(clientId)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult<List<string>> GetSharesListByClientId(int clientId)
        {
            try
            {
                return Ok(this.shareService.GetAllSharesListByTraderId(clientId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("all")]
        [HttpGet]
        public ActionResult<string> GetAllShares(int shareId, int shareTypeId)
        {
            try
            {
                var allShares = this.shareService.GetAllShares();
                return Ok(JsonConvert.SerializeObject(allShares));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("change")]
        [HttpGet]
        public ActionResult<string> ChangeShareType(int shareId, int shareTypeId)
        {
            try
            {
                this.shareService.ChangeShareType(shareId, shareTypeId);
                return Ok("Share type changed");
            }
            catch (Exception ex)
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
