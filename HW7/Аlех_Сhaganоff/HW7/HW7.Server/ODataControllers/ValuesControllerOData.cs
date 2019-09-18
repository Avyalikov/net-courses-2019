using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW7.Core;
using HW7.Core.Repositories;
using HW7.Core.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Server.Controllers
{
    [Route("odata")]
    [Route("odata/[controller]")]
    [ApiController]
    public class ValuesControllerOData : ODataController
    {
        private readonly IContextProvider _context;

        public ValuesControllerOData(IContextProvider context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Odata" };
        }
    }
}
