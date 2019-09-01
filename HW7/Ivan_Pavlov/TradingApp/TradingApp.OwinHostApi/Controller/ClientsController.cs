namespace TradingApp.OwinHostApi.Controller
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;
    using TradingApp.Core.Services;
    using TradingApp.Core.ServicesInterfaces;
    using TradingApp.OwinHostApi.Repositories;

    public class ClientsController : ApiController
    {
        private readonly IUsersService usersService;
        
        public ClientsController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IHttpActionResult GetSomeUsers(int top, int page)
        {
            var users = usersService.GetAllUsers().ToList();
            if (top > users.Count)
            {
                top = users.Count;
            }
            int maxPage = users.Count / top;
            if (page > maxPage)
                page = maxPage;

            List<UserEntity> result = new List<UserEntity>();          
            for(int i = top * (page - 1); i < top * page; i++)
            {
                if (i == users.Count)
                    break;
                result.Add(users[i]);
            }
            return Json(result);
        }

        [ActionName("add")]
        public void PostAdd(JObject json)
        {
            var user = JsonConvert.DeserializeObject<UserInfo>(json.ToString());
    
            usersService.AddNewUser(user);          
        }

        [ActionName("update")]
        public void PutUpdateUser(int id, JObject json)
        {
            var userBalance = usersService.GetUserById(id).Balance;
            var user = JsonConvert.DeserializeObject<UserInfo>(json.ToString());
            user.Balance = userBalance;
            usersService.Update(id, user);

            decimal balance = decimal.Parse(json.Value<string>("Balance"));
            if (balance != 0)
                usersService.ChangeUserBalance(id, balance);
        }

        [ActionName("remove")]
        public void DeleteUser(int id)
        {
            usersService.Remove(id);
        }
    }
}
