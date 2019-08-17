using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace TestApplicationServer.Controllers
{
    public class SubData
    {
        public string DataA { get; set; }
        public string DataB { get; set; }
    }

    public class ValuesRequestData
    {
        public string PropA { get; set; }
        public string PropB { get; set; }
        public SubData SomeSubDataB { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] ValuesRequestData value)
        {
            return new ActionResult<string>("testResponse");
        }
    }
}
