using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Client
{
    public class ODataRequestsProvider : IRequestsProvider
    {
        private string connectionString = "http://localhost:5000/odata/";
       
        public string ConnectionString { get => connectionString; set => connectionString = value; }
       
        private void Get(string url)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Console.WriteLine(result);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void Post(string url, string body)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(body);
            }
            
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Console.WriteLine(result);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void PostWithoutResult(string url, string body)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(body);
            }

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    //Console.WriteLine(result);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void Put(string url, string body)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(body);
            }

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Console.WriteLine(result);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void Delete(string url, string body)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "DELETE";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(body);
            }

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Console.WriteLine(result);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void CheckConnection(string url)
        {
            Get(url);
        }

        public void GetListOfClients()
        {
            int top = 10;
            int page = 0;

            Console.WriteLine("Please enter page");
            page = GetIntFromInput();

            string url = ConnectionString + @"clients?$skip=" + (page * 10 - 10) + @"&$top=" + top;

            Get(url);
        }

        public void AddClient()
        {
            string FirstName;
            string LastName;
            string PhoneNumber;
            decimal Balance;

            Console.WriteLine("Please enter first name");
            FirstName = Console.ReadLine();
            Console.WriteLine("Please enter last name");
            LastName = Console.ReadLine();
            Console.WriteLine("Please enter phone number");
            PhoneNumber = Console.ReadLine();
            Console.WriteLine("Please enter balance");
            Balance = GetDecimalFromInput();

            string url = ConnectionString + @"clients/add";
            string body = @"{""FirstName"": """ + FirstName + @""", " +
                          @"""LastName"": """ + LastName + @""", " +
                          @"""PhoneNumber"": """ + PhoneNumber + @""", " +
                          @"""Balance"": """ + Balance + @"""}";

            Post(url, body);
        }

        public void UpdateClient()
        {
            int TraderId;
            string FirstName;
            string LastName;
            string PhoneNumber;
            decimal Balance;

            Console.WriteLine("Please enter client id");
            TraderId = GetIntFromInput();
            Console.WriteLine("Please enter first name");
            FirstName = Console.ReadLine();
            Console.WriteLine("Please enter last name");
            LastName = Console.ReadLine();
            Console.WriteLine("Please enter phone number");
            PhoneNumber = Console.ReadLine();
            Console.WriteLine("Please enter balance");
            Balance = GetDecimalFromInput();

            string url = ConnectionString + @"clients/update";
            string body = @"{
                ""TraderId"": """ + TraderId + @""", " +
                          @"""FirstName"": """ + FirstName + @""", " +
                          @"""LastName"": """ + LastName + @""", " +
                          @"""PhoneNumber"": """ + PhoneNumber + @""", " +
                          @"""Balance"": """ + Balance + @"""}";

            Put(url, body);
        }

        public void RemoveClient()
        {
            int TraderId;

            Console.WriteLine("Please enter client id");
            TraderId = GetIntFromInput();

            string url = ConnectionString + @"clients/remove";
            string body = @"{""TraderId"": """ + TraderId + @"""}";

            Delete(url, body);
        }

        public void GetListOfShares()
        {
            int traderId;

            Console.WriteLine("Please enter client id");
            traderId = GetIntFromInput();

            string url = ConnectionString + @"clients/portfolioswithprices?$filter=TraderId eq " + traderId;

            Get(url);
        }

        public void AddShare()
        {
            string Name;
            decimal Price;

            Console.WriteLine("Please enter name");
            Name = Console.ReadLine();
            Console.WriteLine("Please enter price");
            Price = GetDecimalFromInput();

            string url = ConnectionString + @"shares/add";
            string body = @"{""Name"": """ + Name + @""", " +
                          @"""Price"": """ + Price + @"""}";

            Post(url, body);
        }

        public void UpdateShare()
        {
            int ShareId;
            string Name;
            decimal Price;

            Console.WriteLine("Please enter share id");
            ShareId = GetIntFromInput();
            Console.WriteLine("Please enter name");
            Name = Console.ReadLine();
            Console.WriteLine("Please enter price");
            Price = GetDecimalFromInput();

            string url = ConnectionString + @"shares/update";
            string body = @"{""ShareId"": """ + ShareId + @""", " +
                          @"""Name"": """ + Name + @""", " +
                          @"""Price"": """ + Price + @"""}";

            Put(url, body);
        }

        public void RemoveShare()
        {
            int ShareId;

            Console.WriteLine("Please enter share id");
            ShareId = GetIntFromInput();

            string url = ConnectionString + @"shares/remove";
            string body = @"{""ShareId"": """ + ShareId + @"""}";

            Delete(url, body);
        }

        public void GetBalance()
        {
            int traderId;

            Console.WriteLine("Please enter client id");
            traderId = GetIntFromInput();

            string url = ConnectionString + @"balances?$filter=TraderId eq " + traderId;

            Get(url);
        }

        public void GetTransactions()
        {
            int traderId;
            int numberOfTransactions;

            Console.WriteLine("Please enter client id");
            traderId = GetIntFromInput();
            Console.WriteLine("Please enter number of transactions to show");
            numberOfTransactions = GetIntFromInput();

            string url = ConnectionString + @"transactions?$filter=BuyerId eq " + traderId +
                                            @" or BuyerId eq " + traderId +
                                            @"&$top=" + numberOfTransactions;

            Get(url);
        }

        public void MakeDeal()
        {
            int SellerId;
            int BuyerId;
            int ShareId;
            int Quantity;

            Console.WriteLine("Please enter seller id");
            SellerId = GetIntFromInput();
            Console.WriteLine("Please enter buyer id");
            BuyerId = GetIntFromInput();
            Console.WriteLine("Please enter share id");
            ShareId = GetIntFromInput();
            Console.WriteLine("Please enter quantity");
            Quantity = GetIntFromInput();


            string url = ConnectionString + @"deal/make";
            string body = @"{""SellerId"": """ + SellerId + @""", " +
                          @"""BuyerId"": """ + BuyerId + @""", " +
                          @"""ShareId"": """ + ShareId + @""", " +
                          @"""Quantity"": """ + Quantity + @"""}";

            Post(url, body);
        }

        public void MakeDeal((int sellerId, int buyerId, int shareId, int purchaseQuantity) data)
        {
            int SellerId = data.sellerId;
            int BuyerId = data.buyerId;
            int ShareId = data.shareId;
            int Quantity = data.purchaseQuantity;

            string url = ConnectionString + @"deal/make";
            string body = @"{""SellerId"": """ + SellerId + @""", " +
                          @"""BuyerId"": """ + BuyerId + @""", " +
                          @"""ShareId"": """ + ShareId + @""", " +
                          @"""Quantity"": """ + Quantity + @"""}";

            PostWithoutResult(url, body);
        }

        int GetIntFromInput()
        {
            int inputValue = 0;
            bool inputCheck = true;

            do
            {
                try
                {
                    inputCheck = true;
                    string input = Console.ReadLine();
                    inputValue = Convert.ToInt32(input);
                    if (inputValue < 0 || inputValue == 0)
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Incorrect input");
                    Console.WriteLine("Please enter a single integer value");
                    inputCheck = false;
                }
            }
            while (inputCheck == false);

            return inputValue;
        }

        decimal GetDecimalFromInput()
        {
            decimal inputValue = 0M;
            bool inputCheck = true;

            do
            {
                try
                {
                    inputCheck = true;
                    string input = Console.ReadLine();
                    inputValue = Convert.ToDecimal(input);
                }
                catch (Exception)
                {
                    Console.WriteLine("Incorrect input");
                    Console.WriteLine("Please enter a single decimal value");
                    inputCheck = false;
                }
            }
            while (inputCheck == false);

            return inputValue;
        }
    }
}
