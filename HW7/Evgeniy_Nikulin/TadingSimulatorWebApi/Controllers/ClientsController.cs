namespace TadingSimulatorWebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using TradingSimulator.Core.Interfaces;

    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ITraderService traderService;
        public ClientsController(ITraderService traderService) =>
            this.traderService = traderService;

        // GET: clients?top=_&page=_
        [HttpGet]
        public string Get(int top, int page)
        {
            return JsonConvert.SerializeObject(traderService.GetTradersPerPage(top, page));
        }

        // GET: /clients/count
        [HttpGet("count")]
        public string GetCount()
        {
            return traderService.GetTraderCount().ToString();
        }

        // GET: /clients/list
        [HttpGet("list")]
        public string GetList()
        {
            return JsonConvert.SerializeObject(traderService.TradersList);
        }

        // GET: /clients/greenlist
        [HttpGet("greenlist")]
        public string GetGreenList()
        {
            return JsonConvert.SerializeObject(traderService.GreenList);
        }

        // GET: /clients/orangelist
        [HttpGet("orangelist")]
        public string GetOrangeList()
        {
            return JsonConvert.SerializeObject(traderService.OrangeList);
        }

        // GET: /clients/blacklist
        [HttpGet("blacklist")]
        public string GetBlackList()
        {
            return JsonConvert.SerializeObject(traderService.BlackList);
        }

        // GET: /clients/shares?clientId=
        [HttpGet("shares")]
        public string Get(int clientId)
        {
            return JsonConvert.SerializeObject(traderService.GetShareList(clientId));
        }

        // POST: /clients/add?name=_&surname=_&phone=_&money=_
        [HttpPost("add")]
        public string AddClient(string name, string surname, string phone, string money)
        {
            return traderService.AddTrader(name, surname, phone, money);
        }

        // POST: /clients/remove?clientId=_
        [HttpPost("remove")]
        public void Post(int clientId)
        {
            traderService.Remove(clientId);
        }

        // POST: /clients/update?clientId=_&newName=_&newSurname=_&newPhone=_&newMoney=_
        [HttpPost("update")]
        public string Post(int clientId, string newName, string newSurname, string newPhone, string newMoney)
        {
            return traderService.ChangeTrader(clientId, newName, newSurname, newPhone, newMoney);
        }
    }
}