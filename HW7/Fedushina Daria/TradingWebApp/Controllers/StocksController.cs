using System;
using System.Linq;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;
using System.Web.Http;
using TradingApp.Core.Repositories;
using System.Web;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace TradingWebApi.Controllers
{

    public class StocksController : ApiController
    {
        private readonly IStockTableRepository stocksService;
        private readonly IBalanceTableRepository balancesService;


        public StocksController() { }
        public StocksController(IStockTableRepository stocksService, IBalanceTableRepository balancesService)
        {
            this.stocksService = stocksService;
            this.balancesService = balancesService;
        }

        // GET api/stocks
        //[HttpGet]
        public string Get()
        {
            string stringToreturn = String.Empty;
            String currurl = Request.RequestUri.ToString();
            String querystring;

            // Check to make sure some query string variables
            // exist and if not return full list of users.
            int iqs = currurl.IndexOf('?');
            if (iqs == -1)
            {

                var users = stocksService.GetAll();

                stringToreturn = Newtonsoft.Json.JsonConvert.SerializeObject(users);
            }
            // If query string variables exist, put them in
            // a string.
            else if (iqs >= 0)
            {
                querystring = (iqs < currurl.Length - 1) ? currurl.Substring(iqs + 1) : String.Empty;
                // Parse the query string variables into a NameValueCollection.
                NameValueCollection qscoll = HttpUtility.ParseQueryString(querystring);
                if (qscoll.AllKeys.Contains("clientId"))
                {
                    int clientID;
                    bool ifPageIsInt = int.TryParse(qscoll.Get("clientId"), out clientID);
                    var obj = balancesService.GetAll(clientID);
                    Dictionary<string, decimal> objToReturn = new Dictionary<string, decimal>();

                    foreach (BalanceEntity balance in obj)
                    {
                        string nameOfStockType = stocksService.Get(balance.StockID).Type;
                        decimal price = stocksService.Get(balance.StockID).Price;
                        objToReturn.Add(nameOfStockType, price);
                    }
                    stringToreturn = Newtonsoft.Json.JsonConvert.SerializeObject(objToReturn);
                }
            }
            return stringToreturn;
        }


        // POST api/stocks
        //[HttpPost]
        [ActionName("add")]
        public string Post([FromBody] StockRegistrationInfo value)
        {
            StockEntity stock = new StockEntity()
            {
                Type = value.Type,
                Price = value.Price,
            };
            stocksService.Add(stock);
            stocksService.SaveChanges();
            int id = stocksService.GetId(value);
            return Newtonsoft.Json.JsonConvert.SerializeObject(stocksService.Get(id));
        }

        // PUT api/users/5
        //[HttpPut("{id}")]
        [ActionName("update")]
        public bool Put([FromBody] StockEntity value)
        {
            return stocksService.Update(value);

        }

        // DELETE api/users/5
        //[HttpDelete("{id}")]
        [ActionName("remove")]
        public void Delete([FromBody]int id)
        {
            stocksService.Delete(id);
        }
    }
}