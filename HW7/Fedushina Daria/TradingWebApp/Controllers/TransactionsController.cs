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

    public class TransactionsController : ApiController
    {
        private readonly IHistoryTableRepository transactionsService;


        public TransactionsController() { }
        public TransactionsController(IHistoryTableRepository transactionsService)
        {
            this.transactionsService = transactionsService;
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

                var balances = transactionsService.GetAllFromBase();

                stringToreturn = Newtonsoft.Json.JsonConvert.SerializeObject(balances);
            }
            // If query string variables exist, put them in
            // a string.
            else if (iqs >= 0)
            {
                querystring = (iqs < currurl.Length - 1) ? currurl.Substring(iqs + 1) : String.Empty;
                // Parse the query string variables into a NameValueCollection.
                NameValueCollection qscoll = HttpUtility.ParseQueryString(querystring);
                if (qscoll.AllKeys.Contains("page") && qscoll.AllKeys.Contains("top"))
                {
                    int clientID;
                    bool ifPageIsInt = int.TryParse(qscoll.Get("clientId"), out clientID);
                    int top;
                    bool ifTopIsInt = int.TryParse(qscoll.Get("top"), out top);
                    var obj = transactionsService.GetAll(clientID).Take(top).ToList();
                    stringToreturn = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                }
            }
            return stringToreturn;
        }
    }
}