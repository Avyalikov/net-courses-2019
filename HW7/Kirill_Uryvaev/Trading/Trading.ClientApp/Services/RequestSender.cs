using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Trading.Core.DataTransferObjects;
using Newtonsoft.Json;
using Trading.Core;
using Trading.Core.Services;

namespace Trading.ClientApp
{
    class RequestSender
    {
        private readonly HttpClient client;
        private readonly string baseAddress;
        public RequestSender()
        {
            baseAddress = "http://localhost:9000/";

            client = new HttpClient();
        }

        public IEnumerable<ClientEntity> GetTop10Clients(int page, int top, out string answer)
        {
            string request1 = "clients?top=";
            string request2 = "&page=";
            return GetCommandWithTwoArguments<int, IEnumerable<ClientEntity>>(request1, request2, top, page, out answer);
        }
        public ClientEntity PostAddClient(ClientRegistrationInfo clientInfo, out string answer)
        {
            string request = "clients/add";            
            return PostCommandsWithReturn<ClientRegistrationInfo, ClientEntity>(request, clientInfo, out answer);
        }
        public void PostUpdateClient(ClientEntity clientInfo, out string answer)
        {
            string request = "clients/update";
            PostCommand(request, clientInfo, out answer);
        }
        public void PostRemoveClient(int clientID, out string answer)
        {
            string request = "clients/remove";
            PostCommand(request, clientID, out answer);
        }
        public IEnumerable<ClientsSharesEntity> GetClientsShares(int id, out string answer)
        {
            string request = "shares?clientId=";
            return GetCommand<int, IEnumerable<ClientsSharesEntity>>(request, id, out answer);
        }
        public ClientsSharesEntity PostAddShare(ClientsSharesInfo sharesInfo, out string answer)
        {
            string request = "shares/add";
            return PostCommandsWithReturn<ClientsSharesInfo, ClientsSharesEntity>(request, sharesInfo, out answer);
        }
        public void PostUpdateShare(ClientsSharesEntity shareInfo, out string answer)
        {
            string request = "shares/update";
            PostCommand(request, shareInfo, out answer);
        }
        public void PostRemoveShare(int[] id, out string answer)
        {
            string request = "shares/remove";
            PostCommand(request, id, out answer);
        }

        public BalanceZone GetBalanceZone(int clientID, out string answer)
        {
            string request = "balances?clientId=";
            return GetCommand<int, BalanceZone>(request, clientID, out answer);
        }

        public void PostMakeDeal(TransactionHistoryInfo transactionInfo, out string answer)
        {
            string request = "deal/make";
            PostCommand(request, transactionInfo, out answer);
        }

        public TransactionHistoryEntity GetClientsTransactions(int clientID, int top, out string answer)
        {
            string request1 = "transactions?clientId=";
            string request2 = "&top=";
            return GetCommandWithTwoArguments<int, TransactionHistoryEntity>(request1,request2, clientID, top, out answer);
        }

        private R GetCommand<T,R>(string request, T argument, out string answer)
        {
            var response = client.GetAsync(baseAddress + $"{request}{argument.ToString()}").Result;
            return returnValueFromRequest<R>(response, out answer);
        }

        private R GetCommandWithTwoArguments<T, R>(string request1, string request2, T argument1, T argument2, out string answer)
        {
            var response = client.GetAsync(baseAddress + $"{request1}{argument1.ToString()}{request2}{argument2}").Result;
            return returnValueFromRequest<R>(response, out answer);
        }

        private R PostCommandsWithReturn<T, R>(string request, T content, out string answer)
        {
            HttpContent contentString = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            var response = client.PostAsync(baseAddress + request, contentString).Result;
            return returnValueFromRequest<R>(response, out answer);
        }

        private R returnValueFromRequest<R>(HttpResponseMessage response, out string answer)
        {
            answer = response.ToString();
            var responseContent = response.Content.ReadAsStringAsync();
            R result = default(R);
            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<R>(responseContent.Result);
            }
            answer += responseContent.Result;
            return result;
        }

        private void PostCommand<T>(string request, T content, out string answer)
        {
            string test = JsonConvert.SerializeObject(content);
            HttpContent contentString = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            var response = client.PostAsync(baseAddress + request, contentString).Result;
            answer = response.ToString();
            var responseContent = response.Content.ReadAsStringAsync();
            answer += responseContent.Result;
        }
    }
}
