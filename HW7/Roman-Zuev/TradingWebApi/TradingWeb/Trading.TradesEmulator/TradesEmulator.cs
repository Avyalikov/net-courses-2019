using System;
using System.Net;
using System.Timers;
using Trading.TradesEmulator.Dto;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using Trading.TradesEmulator.Services;

namespace Trading.TradesEmulator
{
    public class TradesEmulator : ITradesEmulator
    {
        private static Timer aTimer;
        private readonly LoggerLog4Net logger;

        public TradesEmulator(LoggerLog4Net logger)
        {
            this.logger = logger;
        }
        public void Run()
        {
            logger.InitLogger();
            SetTimer();
            Console.ReadKey();
        }

        private TransactionArguments GenerateTransactionArguments()
        {

            Random random = new Random();
            var args = new TransactionArguments();
            args.SellerId = random.Next(1,8);
            do
            {
                args.BuyerId = random.Next(1,8);
            } while (args.SellerId == args.BuyerId);
            args.SharesId = random.Next(1,3);
            args.Quantity = random.Next(1,5);
            return args;
        }

        private void StartTrades(Object source, ElapsedEventArgs e)
        {
            var urlSettings = System.Configuration.ConfigurationManager.AppSettings;
            string url = urlSettings["TradingServer"];
            string type = urlSettings["Type"];
            SendRequestWithLogging(GenerateTransactionArguments(), url, type);
        }
        private void SetTimer()
        {
            aTimer = new Timer(20000);
            aTimer.Elapsed += StartTrades;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        private void SendRequestWithLogging(TransactionArguments transactionArgs, string url, string requestType)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = requestType;
                string postData = JsonConvert.SerializeObject(transactionArgs);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                logger.Log.Info($"Generated transaction args: {postData}");
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                logger.Log.Info($"Sending request to {url}");

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                logger.Log.Info($"Request stream closed");

                WebResponse response = request.GetResponse();
                logger.Log.Info($"{((HttpWebResponse)response).StatusCode} {((HttpWebResponse)response).StatusDescription}");
            }
            catch (Exception ex)
            {
                logger.Log.Error(ex.Message);
                logger.Log.Error(ex.StackTrace);
            }
            
        }
    }
}
