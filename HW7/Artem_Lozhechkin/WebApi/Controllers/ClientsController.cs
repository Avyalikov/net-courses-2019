using Microsoft.AspNetCore.Mvc;
using System;
using TradingApp.Core.DTO;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;
using TradingApp.Core.Services;

namespace WebApi.Controllers
{
    [Route("clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IRepository<TraderEntity> clientRepository;
        private readonly TraderService service;

        public ClientsController(IRepository<TraderEntity> clientRepository, TraderService service)
        {
            this.clientRepository = clientRepository;
            this.service = service;
        }
        //[Route("get")]
        //[HttpGet]
        //public ActionResult<List<TraderEntity>> Get(int top, int page)
        //{
        //    return this.clientRepository.GetAll();
        //}
        [Route("add")]
        [HttpPost]
        public IActionResult Register([FromBody] TraderInfo traderInfo)
        {
            try
            {
                this.service.RegisterNewUser(traderInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
