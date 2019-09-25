using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW7.Core;
using HW7.Core.Repositories;
using HW7.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Server.Controllers
{
    [Route("")]
    [Route("api")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IContextProvider _context;

        public ValuesController(IContextProvider context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Connection established: Web API" };
        }
    }
}
