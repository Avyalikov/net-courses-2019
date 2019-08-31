using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Services;
using TradingSimulatorWebApi.Data;

namespace TradingSimulatorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBlackListController : ControllerBase
    {
        TradingSimulatorDbContext db = new TradingSimulatorDbContext();

        // GET: https://localhost:44397/api/userBlackList
        [HttpGet]
        public IEnumerable<UserEntity> Get()
        {
            UserService userService = new UserService(db);
            return userService.GetUserBlackList();
        }
    }
}