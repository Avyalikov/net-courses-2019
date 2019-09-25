using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Client
{
    public class TradingSimulation
    {
        public string ConnectionString { get; private set; }
        public bool SimulationIsWorking { get; set; }

        public TradingSimulation(string connectionString)
        {
            ConnectionString = connectionString;
            SimulationIsWorking = false;
        }

        List<int> GetListOfInt(string url)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            string result;

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();

                    var jobj = JsonConvert.DeserializeObject<List<int>>(result);

                    //var jobj = (List<int>)JsonConvert.DeserializeObject(result);

                    return jobj;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        int GetInt(string url)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            string result;

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();

                    //var jobj = (int)JsonConvert.DeserializeObject(result);

                    var jobj = JsonConvert.DeserializeObject<int>(result);

                    return jobj;
                }
            }
            catch
            {

            }

            return 0;
        }

        List<int> GetAvailableBuyers()
        {
            string url = ConnectionString + @"clients/buyerslist";
            return GetListOfInt(url);
        }

        List<int> GetAvailableSellers()
        {
            string url = ConnectionString + @"clients/sellerslist";
            return GetListOfInt(url);
        }

        List<int> GetAvailableShares(int clientId)
        {
            string url = ConnectionString + @"shares/availableshares?clientId=" + clientId;
            return GetListOfInt(url);
        }

        int GetShareQuantityFromPortfoio(int clientId, int shareId)
        {
            string url = ConnectionString + @"transactions/sharequantity?clientId=" + clientId + @"&shareId=" + shareId;
            return GetInt(url);
        }

        public (int sellerId, int buyerId, int shareId, int purchaseQuantity) PerformRandomOperation()
        {
            int sellerId;
            int buyerId;
            int shareId;
            int purchaseQuantity;

            try
            {
                List<int> availableSellers = GetAvailableSellers();
                List<int> availableBuyers = GetAvailableBuyers();

                if (availableSellers.Count > 0)
                {
                    sellerId = availableSellers[new Random().Next(0, availableSellers.Count)];
                }
                else
                {
                    throw new Exception("No sellers with shares");
                }

                if (availableBuyers.Count > 1)
                {
                    buyerId = availableBuyers[new Random().Next(0, availableBuyers.Count)];
                }
                else
                {
                    throw new Exception("No buyers");
                }

                while (sellerId == buyerId)
                {
                    buyerId = availableBuyers[new Random().Next(0, availableBuyers.Count)];
                }

                if (buyerId == sellerId)
                {
                    throw new Exception("buyerId == sellerId");
                }

                List<int> availableShares = GetAvailableShares(sellerId);

                shareId = availableShares[new Random().Next(0, availableShares.Count)];

                purchaseQuantity = new Random().Next(1, GetShareQuantityFromPortfoio(sellerId, shareId) + 1);

                return (sellerId, buyerId, shareId, purchaseQuantity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return (0, 0, 0, 0);
        }
    }
}
