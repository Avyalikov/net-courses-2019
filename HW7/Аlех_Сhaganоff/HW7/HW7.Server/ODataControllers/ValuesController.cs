using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW7.Core;
using HW7.Core.Repositories;
using HW7.Core.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Server.ODataControllers
{
    [Route("odata")]
    [Route("odata/[controller]")]
    public class ValuesController : ODataController
    {
        private readonly IContextProvider contextprovider;

        public ValuesController(IContextProvider contextprovider)
        {
            this.contextprovider = contextprovider;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Connetction established: Odata" };
        }
    }
}
