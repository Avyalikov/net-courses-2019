namespace TradingApp.ConsoleTradingManager
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using TradingApp.Core.DTO;
    using TradingApp.Core.Models;

    public class RequestSender
    {
        private readonly HttpClient httpClient;
        public RequestSender()
        {
            httpClient = new HttpClient();
        }

        public List<ShareEntity> GetAllSharesByTrader(int traderId)
        {
            var response = httpClient.GetAsync($"https://localhost:5001/shares/client_shares?clientId={traderId}").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<ShareEntity>>(responseBody);
            }
            throw new Exception(responseBody);
        }
        public List<TraderEntity> GetAllTradersList()
        {
            var response = httpClient.GetAsync("https://localhost:5001/clients/all").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<TraderEntity>>(responseBody);
            }
            throw new Exception(responseBody);
        }
        public string MakeTransaction(TransactionInfo transactionInfo)
        {
            var content = new StringContent(JsonConvert.SerializeObject(transactionInfo), Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync("https://localhost:5001/deal/make", content).Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                return responseBody;
            }
            throw new Exception(responseBody);
        }
        public List<TraderEntity> GetBlackStatusClients()
        {
            var response = httpClient.GetAsync("https://localhost:5001/clients/black").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<TraderEntity>>(responseBody);
            }
            throw new Exception(responseBody);
        }
        public List<TraderEntity> GetOrangeStatusClients()
        {
            var response = httpClient.GetAsync("https://localhost:5001/clients/orange").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<TraderEntity>>(responseBody);
            }
            throw new Exception(responseBody);
        }
        public string RegisterTrader(TraderInfo traderInfo)
        {
            var content = new StringContent(JsonConvert.SerializeObject(traderInfo), Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync("https://localhost:5001/clients/add", content).Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return responseBody;
            }
            throw new Exception(responseBody);
        }
        public string ChangeShareType(int shareId, int shareTypeId)
        {
            var response = httpClient.GetAsync($"https://localhost:5001/shares/change?shareId={shareId}&shareTypeId={shareTypeId}").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return responseBody;
            }
            throw new Exception(responseBody);
        }
        public IEnumerable<ShareEntity> GetAllShares()
        {
            var response = httpClient.GetAsync("https://localhost:5001/shares/all").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ShareEntity>>(responseBody);
            }
            throw new Exception(responseBody);
        }
    }
}
