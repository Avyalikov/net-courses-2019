using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Interfaces;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Services;
using WebApiServer.Interfaces;

namespace WebApiServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ITraderService tradersService;
        private readonly IBankruptService bankruptService;
        private readonly IValidator validator;

        public ClientsController(ITraderService tradersService, IBankruptService bankruptService, IValidator validator)
        {
            this.tradersService = tradersService;
            this.bankruptService = bankruptService;
            this.validator = validator;
        }

        // GET /clients?top=_
        [HttpGet]
        public string Get(int top)
        {
            List<string> tradersList = tradersService.GetListOfTraders(top);
            return JsonConvert.SerializeObject(tradersList);
        }

        // GET /clients/get?id=_
        [HttpGet("get")]
        public string GetByID(int id)
        {
            return JsonConvert.SerializeObject(tradersService.GetTraderById(id));
        }

        // GET /clients/count
        [HttpGet("count")]
        public string Get()
        {
            return JsonConvert.SerializeObject(tradersService.GetCountIds());
        }

        // POST: /clients/add?name=_&surname=_&phone=_&
        [HttpPost("add")]
        public ActionResult AddClient(string name, string surname, string phone)
        {
            var newTrader = new TraderInfo()
            {
                Name = name,
                Surname = surname,
                PhoneNumber = phone,
                Balance = 1000M
            };
            if (!validator.TraderInfoValidate(newTrader))
            {
                return StatusCode(400, "Bad values for registration new trader");
            }
            try
            {
                tradersService.RegisterNewTrader(newTrader);
            }
            catch (ArgumentException)
            {
                return StatusCode(400, "This trader has been registered)");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("New trader added succesfully");
        }

        // GET /clients/blackzone
        [HttpGet("blackzone")]
        public string GetBlackList()
        {
            return JsonConvert.SerializeObject(bankruptService.GetListTradersFromBlackZone());
        }

        // GET /clients/orangezone
        [HttpGet("orangezone")]
        public string GetOrangeList()
        {
            return JsonConvert.SerializeObject(bankruptService.GetListTradersFromOrangeZone());
        }
    }        
}