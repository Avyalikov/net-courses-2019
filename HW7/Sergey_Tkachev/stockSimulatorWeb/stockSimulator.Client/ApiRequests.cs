using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using stockSimulator.Core.DTO;

namespace stockSimulator.Client
{
    public class ApiRequests
    {
        const string url = "http://localhost:";
        const string port = "5000/";
        string connectionString = url + port;

        internal string GetListOfClients(int numberOfClientsToPrint, int numberOfPages)
        {
            string request = connectionString + "clients?top=" + numberOfClientsToPrint + "&page=" + numberOfPages;
            string result = Get(request);
            return result;
        }

        internal string AddNewClient(ClientRegistrationInfo newClient)
        {
            string request = connectionString + "clients/add";
            string jsonData = JsonConvert.SerializeObject(newClient, Formatting.Indented);
            var result = Task.Run(() => Post(request, jsonData));
            return result.Result;
        }

        internal string UpdateClient(UpdateClientInfo changedClient)
        {
            string request = connectionString + "clients/update";
            string jsonData = JsonConvert.SerializeObject(changedClient, Formatting.Indented);
            var result = Task.Run(() => Post(request, jsonData));
            return result.Result;
        }

        internal string RemoveClient(int clientId)
        {
            string request = connectionString + "clients/remove";
            string jsonData = JsonConvert.SerializeObject(clientId, Formatting.Indented);
            var result = Task.Run(() => Post(request, jsonData));
            return result.Result;
        }

        internal string GetStateOfClient(int clientId)
        {
            string request = connectionString + "balances?clientId=" + clientId;
            string result = Get(request);
            return result;
        }

        internal string GetListOfClientTransactions(int clientId, int numOfTransactions)
        {
            string request = connectionString + "transactions?clientId=" + clientId + "&top=" + numOfTransactions;
            string result = Get(request);
            return result;
        }

        internal string AddStockToClient(EditStockOfClientInfo editStockOfClientInfo)
        {
            string request = connectionString + "shares/add";
            string jsonData = JsonConvert.SerializeObject(editStockOfClientInfo, Formatting.Indented);
            var result = Task.Run(() => Post(request, jsonData));
            return result.Result;
        }

        internal string RemoveStockOfClient(EditStockOfClientInfo removeStockOfClientInfo)
        {
            string request = connectionString + "shares/remove";
            string jsonData = JsonConvert.SerializeObject(removeStockOfClientInfo, Formatting.Indented);
            var result = Task.Run(() => Post(request, jsonData));
            return result.Result;
        }

        internal string EditStockInfoOfClient(EditStockOfClientInfo editStockOfClientInfo)
        {
            string request = connectionString + "shares/update";
            string jsonData = JsonConvert.SerializeObject(editStockOfClientInfo, Formatting.Indented);
            var result = Task.Run(() => Post(request, jsonData));
            return result.Result;
        }

        internal string MakeDeal(TradeInfo tradeInfo)
        {
            string request = connectionString + "deal/make";
            string jsonData = JsonConvert.SerializeObject(tradeInfo, Formatting.Indented);
            var result = Task.Run(() => Post(request, jsonData));
            return result.Result;
        }

        internal string GetListOfStocksOfClient(int clientId)
        {
            string request = connectionString + "shares?clientId=" + clientId;
            string result = Get(request);
            return result;
        }

        private string Post(string url, string data)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = client.PostAsync(url, content).Result;
                return result.Content.ReadAsStringAsync().Result;
            }
        }

        private string Get(string url)
        {
            // Create a request for the URL.   
            WebRequest request = WebRequest.Create(url);
           
            WebResponse response = null;
            try
            {
                // Get the response.  
                response = request.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to get response! Error: " + ex.Message);
            }

            if (response != null)
            {
                // Display the status.  
                Console.WriteLine("Server answer: " + ((HttpWebResponse)response).StatusDescription);

                // Get the stream containing content returned by the server. 
                // The using block ensures the stream is automatically closed. 
                using (Stream dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.  
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.  
                    string responseFromServer = reader.ReadToEnd();
                    response.Close();
                    // Return the content.  
                    return responseFromServer;
                }
            }
            return null;
        }

       
    }
}
