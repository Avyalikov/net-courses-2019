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

    public class BalancesController : ApiController
    {
        private readonly IStockTableRepository stocksService;
        private readonly IBalanceTableRepository balancesService;


        public BalancesController() { }
        public BalancesController(IStockTableRepository stocksService, IBalanceTableRepository balancesService)
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

                var balances = balancesService.GetAllFromBase();

                stringToreturn = Newtonsoft.Json.JsonConvert.SerializeObject(balances);
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
                    string nameOfGroup = String.Empty;
                    foreach (BalanceEntity balance in obj)
                    {
                        if(balance.Balance<0) 
                            nameOfGroup = "Black List";
                        if (balance.Balance == 0)
                            nameOfGroup = "Orange List";
                        if (balance.Balance >= 0)
                            nameOfGroup = "Green";
                        decimal balanceSum = balance.Balance;
                        objToReturn.Add(nameOfGroup, balanceSum);
                    }
                    stringToreturn = Newtonsoft.Json.JsonConvert.SerializeObject(objToReturn);
                }
            }
            return stringToreturn;
        }
    }
}