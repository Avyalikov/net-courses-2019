using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.Client.Dto;

namespace TradeSimulator.Client.Modules
{
    internal class RequestSenderModule
    {
        private readonly HttpClient httpClient;
        private readonly string baseAddress;
        public RequestSenderModule()
        {
            httpClient = new HttpClient();
            baseAddress = ConfigurationManager.AppSettings["WebTradeSimulatotServiceAddress"];
        }

        public string RegisterNewClient(RegInfo regInfo)
        {
            var content = new StringContent(JsonConvert.SerializeObject(regInfo), Encoding.UTF8, "application/json");
            
            var response = this.httpClient.PostAsync(this.baseAddress + "/clients/add", content).Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return responseBody;
            }
            throw new Exception(responseBody);
        }

        public string MakeATrade(TransactionInfo transactionInfo)
        {
            var content = new StringContent(JsonConvert.SerializeObject(transactionInfo), Encoding.UTF8, "application/json");

            var response = this.httpClient.PostAsync(this.baseAddress + "/deal/make", content).Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return responseBody;
            }
            throw new Exception(responseBody);
        }
               
        public List<ClientInfo> GetPageOfClientsList(int lengthOfPage, int numberOfPage)
        {
            var response = this.httpClient.GetAsync(this.baseAddress + $"/clients?top={lengthOfPage}&page={numberOfPage}").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<ClientInfo>>(responseBody);
            }
            throw new Exception(responseBody);
        }

        public List<ClientInfo> GetAllClients()
        {
            var response = this.httpClient.GetAsync(this.baseAddress + $"/clients/all").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<ClientInfo>>(responseBody);
            }
            throw new Exception(responseBody);
        }

        public List<StockOfClientInfo> GetAllStocksOfClientsList()
        {
            var response = this.httpClient.GetAsync(this.baseAddress + "/shares/all").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {               
                return JsonConvert.DeserializeObject<List<StockOfClientInfo>>(responseBody);
            }
            throw new Exception(responseBody);
        }

        public List<StockOfClientInfo> GetAllStocksOfClientListByClientId(int clientId)
        {
            var response = this.httpClient.GetAsync(this.baseAddress + $"/shares?clientId={clientId}").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<StockOfClientInfo>>(responseBody);
            }
            throw new Exception(responseBody);
        }

        public AccountInfo GetAccountByClientId(int clientId)
        {
            var response = this.httpClient.GetAsync(this.baseAddress + $"/balance?clientId={clientId}").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<AccountInfo>(responseBody);
            }
            throw new Exception(responseBody);
        }

        public List<HistoryInfo> GetNClientsHistoryRecords(int clientId, int top)
        {
            var response = this.httpClient.GetAsync(this.baseAddress + $"/transaction?clientId={clientId}&top={top}").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<HistoryInfo>>(responseBody);
            }
            throw new Exception(responseBody);
        }

        public ClientInfo GetClientByNameAndSurname(string name, string surname)
        {
            var response = this.httpClient.GetAsync(this.baseAddress + $"/clients/clinet?name={name}&surname={surname}").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ClientInfo>(responseBody);
            }
            throw new Exception(responseBody);
        }

        public bool GetCheckResultIfStockPriceAlreadyExists(string type)
        {
            var response = this.httpClient.GetAsync(this.baseAddress + $"/shares/check?type={type}").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(responseBody);
            }
            throw new Exception(responseBody);
        }

        public bool GetCheckIfBlackZoneIsNotEmpty()
        {
            var response = this.httpClient.GetAsync(this.baseAddress + $"/clients/checkzone").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(responseBody);
            }
            throw new Exception(responseBody);
        }        
    }
}
