namespace TradingApp.OwinHostApi.Controller
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Web.Http;
    using TradingApp.Core.ServicesInterfaces;

    public class BalancesController : ApiController
    {
        private readonly IUsersService usersService;
      
        public BalancesController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IHttpActionResult GetUsersBalance(int userId)
        {
            var user = usersService.GetUserById(userId);
            if (user == null)
                return Json("Запрашиваемый пользователь не найден");
            string userName = $"{user.Name} {user.SurName} имеет ";
            if (user.Balance > 0)
                return Json(new string[] { userName, user.Balance.ToString(), " и зеленый статус" });
            else if (user.Balance == 0)
                return Json(new string[] { userName, user.Balance.ToString(), " и оранжевый статус" });
            else
                return Json(new string[] { userName, user.Balance.ToString(), " и черный статус" });
        }      
    }
}
