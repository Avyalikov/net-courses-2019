namespace Traiding.ConsoleApp.DependencyInjection
{
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Traiding.ConsoleApp.Dto;
    using Traiding.ConsoleApp.Models;

    public class RequestSender
    {
        private readonly HttpClient client;
        public RequestSender()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri("http://localhost:52804");
        }
        
        private R Post<T, R>(string reqString, T content)
        {
            var jsonString = JsonConvert.SerializeObject(content);
            HttpContent contentString = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = client.PostAsync(reqString, contentString).Result;
            return ReturnValueFromRequest<R>(response);
        }

        private R Get<R>(string reqString)
        {
            var resp = client.GetAsync(reqString).Result;
            return ReturnValueFromRequest<R>(resp);
        }

        private R ReturnValueFromRequest<R>(HttpResponseMessage response)
        {
            var responseContent = response.Content.ReadAsStringAsync();
            R result = default(R);
            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<R>(responseContent.Result);
            }            
            return result;
        }

        public string AddClient(ClientInputData clientInputData)
        {
            string request = "clients/add";
            return Post<ClientInputData, string>(request, clientInputData);
        }

        public string EditClient(ClientInputData clientInputData)
        {
            string request = "clients/update";
            return Post<ClientInputData, string>(request, clientInputData);
        }

        public string RemoveClient(int clientId)
        {
            string request = "clients/remove";
            return Post<int, string>(request, clientId);
        }

        public string AddShare(ShareInputData shareInputData)
        {
            string request = "share/add";
            return Post<ShareInputData, string>(request, shareInputData);
        }

        public string EditShare(ShareInputData shareInputData)
        {
            string request = "share/update";
            return Post<ShareInputData, string>(request, shareInputData);
        }

        public string RemoveShare(int shareId)
        {
            string request = "share/remove";
            return Post<int, string>(request, shareId);
        }

        public string GetBalanceZoneColor(int clientId)
        {
            string request = $"balances?clientId={clientId}";
            return Get<string>(request);
        }

        public IEnumerable<OperationEntity> GetClientOperations(int clientId, int top)
        {
            string request = $"transactions?clientId={clientId}&top={top}";
            return Get<IEnumerable<OperationEntity>>(request);
        }

        public IEnumerable<ClientEntity> GetClients(int number, int page)
        {
            string request = $"clients?top={number}&page={page}";
            return Get<IEnumerable<ClientEntity>>(request);
        }

        public IEnumerable<SharesNumberEntity> GetSharesNumbersByClientId(int clientId)
        {
            string request = $"shares?clientId={clientId}";
            return Get<IEnumerable<SharesNumberEntity>>(request);
        }

        public string Deal(OperationInputData operationInputData)
        {
            string request = "deal/make";
            return Post<OperationInputData, string>(request, operationInputData);
        }
    }
}
