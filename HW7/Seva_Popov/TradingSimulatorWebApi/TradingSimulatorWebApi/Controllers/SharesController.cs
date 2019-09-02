using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TradingSimulator.Core.Models;
using TradingSimulatorConsoleApp.Data;
using TradingSimulatorConsoleApp.Services;


namespace TradingSimulatorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharesController : ControllerBase
    {
        TradingSimulatorDbContext db = new TradingSimulatorDbContext();

        // GET:    https://localhost:44397/api/shares
        [HttpGet]
        public IEnumerable<SharesEntity> Get()
        {
            SharesService sharesService = new SharesService(db);
            return sharesService.Get();
        }

        // PUT:    Invoke-RestMethod https://localhost:44397/api/shares -Method PUT -Body (@{Id = 5; Name = "MTSS"; Price = 266;} | ConvertTo-Json) -ContentType "application/json"
        [HttpPut]
        public IActionResult Put([FromBody]SharesEntity shares)
        {

            SharesService sharesService = new SharesService(db);
            return Ok(sharesService.Put(shares));
        }

        // POST:   Invoke-RestMethod https://localhost:44397/api/shares -Method POST -Body (@{Name = "MSFT"; Price = 137;} | ConvertTo-Json) -ContentType "application/json"
        
        [HttpPost]
        public IActionResult Post([FromBody]SharesEntity shares)
        {
            SharesService sharesService = new SharesService(db);
            return Ok(sharesService.Add(shares));
        }

        // DELETE:   Invoke-RestMethod  https://localhost:44397/api/shares/4 -Method DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SharesService sharesService = new SharesService(db);
            return Ok(sharesService.Delete(id));
        }
    }
}
