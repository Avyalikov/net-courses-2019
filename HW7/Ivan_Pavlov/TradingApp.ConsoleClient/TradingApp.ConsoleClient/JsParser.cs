namespace TradingApp.ConsoleClient
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using TradingApp.ConsoleClient.JsModels;

    public static class JsParser
    {
        public static ICollection<JsUser> ParseUsers(string jsString)
        {
            var jsArray = (JArray)JsonConvert.DeserializeObject<Object>(jsString);
            List<JsUser> result = JsonConvert.DeserializeObject<List<JsUser>>(jsString);

            return result;
        }

        public static ICollection<JsShare> ParseShares(string jsString)
        {
            var jsArray = (JArray)JsonConvert.DeserializeObject<Object>(jsString);
            var shares = new List<JsShare>();
            foreach (var jsOb in jsArray)
            {
                shares.Add(new JsShare()
                {
                    Name = jsOb.Value<string>("Name"),
                    CompanyName = jsOb.Value<string>("CompanyName"),
                    Price = jsOb.Value<decimal>("Price")
                });
            }

            return shares;
        }

        public static JsBalance ParseBalance(string jsString)
        {
            var jsArray = (JArray)JsonConvert.DeserializeObject<Object>(jsString);
            var balance = new JsBalance()
            {
                UserInfo = jsArray[0].Value<string>(),
                Balance =  decimal.Parse(jsArray[1].Value<string>()),
                InfoAboutStatus = jsArray[2].Value<string>()
            };
            return balance;
        }

        public static ICollection<JsTransactionStory> ParseTransaction(string jsString)
        {
            var jsArray = (JArray)JsonConvert.DeserializeObject<Object>(jsString);
            var transactions = new List<JsTransactionStory>();
            foreach (var jsOb in jsArray)
            {
                transactions.Add(new JsTransactionStory()
                {
                    SellerId = jsOb.Value<int>("SellerId"),
                    CustomerId = jsOb.Value<int>("CustomerId"),
                    ShareId = jsOb.Value<int>("ShareId"),
                    DateTime = jsOb.Value<DateTime>("DateTime"),
                    AmountOfShares = jsOb.Value<int>("AmountOfShares"),
                    TransactionCost = jsOb.Value<decimal>("TransactionCost")
                });
            }
            return transactions;
        }
    }
}
