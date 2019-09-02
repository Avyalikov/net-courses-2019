using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Services;
using TradingSimulatorConsoleApp.Data;
using TradingSimulatorConsoleApp.Services;


namespace TradingSimulatorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {     
        TradingSimulatorDbContext db = new TradingSimulatorDbContext();

        // GET: https://localhost:44397/api/user
        [HttpGet]
        public IEnumerable<UserEntity> Get()
        {
            UserService userService = new UserService(db);
            return userService.Get();
        }

        // PUT: Invoke-RestMethod https://localhost:44397/api/user -Method PUT -Body (@{Id = 5; Surname = "Bob"; Name = "Marley"; Balance = 1000001; Phone = "112-112-2"} | ConvertTo-Json) -ContentType "application/json"
        [HttpPut]
        public IActionResult Put([FromBody]UserEntity user)
        {

            UserService userService = new UserService(db);
            return Ok(userService.Put(user));
        }

        // POST: Invoke-RestMethod https://localhost:44397/api/user -Method POST -Body (@{Surname = "Bob"; Name = "Yellow"; Balance = 1000000; Phone = "111-111-1"} | ConvertTo-Json) -ContentType "application/json"

        [HttpPost]
        public IActionResult Post([FromBody]UserEntity user)
        {
            UserService userService = new UserService(db);
            return Ok(userService.Add(user));
        }


        // DELETE: Invoke-RestMethod  https://localhost:44397/api/user/5 -Method DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                UserService userService = new UserService(db);
                return Ok(userService.Delete(id));
            }
            catch (Exception)
            {
                throw;
            }
        }       
    }
}
