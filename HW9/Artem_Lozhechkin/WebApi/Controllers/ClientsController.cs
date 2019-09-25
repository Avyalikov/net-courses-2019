namespace TradingApp.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using TradingApp.Core.DTO;
    using TradingApp.Core.Models;
    using TradingApp.Core.Services;

    [Route("clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly TraderService traderService;

        public ClientsController(TraderService service)
        {
            this.traderService = service;
        }
        [Route("add")]
        [HttpPost]
        public IActionResult RegisterUser([FromBody] TraderInfo traderInfo)
        {
            try
            {
                this.traderService.RegisterNewUser(traderInfo);
                return Ok("User successfully added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("update")]
        [HttpPost]
        public IActionResult UpdateUser([FromBody] TraderEntity traderEntity)
        {
            try
            {
                this.traderService.UpdateUser(traderEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [Route("remove")]
        [HttpPost]
        public IActionResult Remove([FromBody] TraderInfo traderInfo)
        {
            try
            {
                this.traderService.RemoveUser(traderInfo);
                return Ok("Successfully removed user from database");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetTop(int top, int page)
        {
            try
            {
                return Ok(this.traderService.GetUserLists(top, page));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("all")]
        [HttpGet]
        public ActionResult<string> GetAll()
        {
            try
            {
                var responseContent = JsonConvert.SerializeObject(this.traderService.GetAllTraders());
                return Ok(responseContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("black")]
        [HttpGet]
        public ActionResult<string> GetBlackStatusClients()
        {
            try
            {
                var responseContent = JsonConvert.SerializeObject(this.traderService.GetTradersFromBlackZone());
                return Ok(responseContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("orange")]
        [HttpGet]
        public ActionResult<string> GetOrangeStatusClients()
        {
            try
            {
                var responseContent = JsonConvert.SerializeObject(this.traderService.GetTradersFromOrangeZone());
                return Ok(responseContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
