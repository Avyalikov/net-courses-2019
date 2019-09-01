using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using TradingSimulator.Core.Services;

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ITraderService tradersService;

        public ValuesController(ITraderService tradersService)
        {
            this.tradersService = tradersService;
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            List<int> ids = tradersService.GetList();
            return JsonConvert.SerializeObject(ids);
        }




        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return $"value + {id}";
        }


    }

    [Route("api/[controller]")]
    [ApiController]
    public class TopController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public List<string> Get()
        {
            return new List<string>() { "value1", "value2" };
        }



        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return $"value - {id}";
        }


    }
}
