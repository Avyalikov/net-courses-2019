using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;

namespace Trading
{
    class TradeManager : ITrade
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IIOProvider ioProvider;
        private readonly IValidator validator;
        private readonly IOperations operations;

        Thread clientsOperation;

        public TradeManager(IPhraseProvider phraseProvider, IIOProvider ioProvider, IValidator validator, IOperations operations)
        {
            this.phraseProvider = phraseProvider;
            this.ioProvider = ioProvider;
            this.validator = validator;
            this.operations = operations;

            clientsOperation = new Thread(operations.StartTradingOperations);
        }

        public void Run()
        {
            Logger.InitLogger();
            string userInput = "";
            ioProvider.WriteLine(phraseProvider.GetPhrase("Welcome"));
            Logger.MainLog.Info("Program started");
            using (var db = new TradingDBContext())
            {
                db.Database.Initialize(false);
                clientsOperation.Start(db);
                while (!userInput.ToLower().Equals("e"))
                {
                    userInput = ioProvider.ReadLine();
                    Logger.MainLog.Info($"User input: {userInput}");
                    Logger.RunWithExceptionLogging(()=>processUserInput(db, userInput));
                }
            }
            Logger.MainLog.Info("Program ended");
        }

        private void processUserInput(TradingDBContext db, string userInput)
        {
            string[] splitedUserInput = userInput.Split(' ', '\t', ';');
            bool isSuccess = true;
            switch (splitedUserInput[0].ToLower())
            {
                case "addclient":
                    isSuccess = addNewClient(db, splitedUserInput);
                    break;
                case "addshare":
                    isSuccess = addNewShare(db, splitedUserInput);
                    break;
                case "changesharestoclient":
                    isSuccess = addSharesToClient(db, splitedUserInput);
                    break;
                case "changeclientmoney":
                    isSuccess = addMoneyToClient(db, splitedUserInput);
                    break;
                case "showorange":
                    isSuccess = showOrangeList(db);
                    break;
                case "showblack":
                    isSuccess = showBlackList(db);
                    break;
                case "showfullclients":
                    isSuccess = showFullClientsList(db);
                    break;
                case "showfullshares":
                    isSuccess = showFullSharesList(db);
                    break;
                case "help":
                    ioProvider.WriteLine(phraseProvider.GetPhrase("Help"));
                    isSuccess = true;
                    break;
                case "e":
                    isSuccess = true;
                    break;
                default:
                    ioProvider.WriteLine("Unknown command");
                    Logger.MainLog.Warn("Unknown command");
                    return;
            }
            if (isSuccess)
            {
                ioProvider.WriteLine("Success");
            }
            else
            {
                ioProvider.WriteLine("Provided data not correct, check log for details");
            }
        }

        private bool addNewClient(TradingDBContext db, string[] clientInfo)
        {
            if (validator.ValidateClient(clientInfo))
            {
                Clients client = new Clients()
                {
                    ClientFirstName = clientInfo[1],
                    ClientLastName = clientInfo[2],
                    PhoneNumber = clientInfo[3],
                    ClientBalance = decimal.Parse(clientInfo[4])
                };
                db.Clients.Add(client);
                db.SaveChanges();
                Logger.MainLog.Info("Successfully added client");
                return true;
            }
            return false;
        }

        private bool addNewShare(TradingDBContext db, string[] shareInfo)
        {
            if (validator.ValidateShare(shareInfo))
            {
                Shares share = new Shares()
                {
                    ShareName = shareInfo[1],
                    ShareCost = decimal.Parse(shareInfo[2]),
                };
                db.Shares.Add(share);
                db.SaveChanges();
                Logger.MainLog.Info("Successfully added share");
                return true;
            }
            return false;
        }

        private bool addSharesToClient(TradingDBContext db, string[] shareToClientInfo)
        {
            if (validator.ValidateShareToClient(db, shareToClientInfo))
            {
                int clientID = int.Parse(shareToClientInfo[1]);
                int shareID = int.Parse(shareToClientInfo[2]);
                int amount = int.Parse(shareToClientInfo[3]);

                ClientsShares sharesClient = db.ClientsShares.Where(x => x.ClientID == clientID && x.ShareID == shareID).FirstOrDefault();
                if (sharesClient == null)
                {
                    sharesClient = new ClientsShares() { ClientID = clientID, ShareID = shareID, Amount = amount };
                    db.ClientsShares.Add(sharesClient);
                }
                else
                {
                    sharesClient.Amount += amount;
                }
                db.SaveChanges();
                Logger.MainLog.Info("Successfully added shares to client");
                return true;
            }
            return false;
        }

        private bool addMoneyToClient(TradingDBContext db, string[] clientInfo)
        {
            if (validator.ValidateClientMoney(clientInfo))
            {
                int clientID = int.Parse(clientInfo[1]);
                decimal amount = decimal.Parse(clientInfo[2]);

                var client = db.Clients.Where(x => x.ClientID == clientID).FirstOrDefault();
                client.ClientBalance += amount;
                db.SaveChanges();
                Logger.MainLog.Info("Successfully added money to client");
                return true;
            }
            return false;
        }

        private bool showOrangeList(TradingDBContext db)
        {
            var orangeList = db.Clients.Where(x => x.ClientBalance == 0).ToList();
            ioProvider.WriteLine(phraseProvider.GetPhrase("OrangeList"));
            showClientsList(orangeList);
            Logger.MainLog.Info("Successfully showed orange list");
            return true;
        }

        private bool showBlackList(TradingDBContext db)
        {
            var blackList = db.Clients.Where(x => x.ClientBalance < 0).ToList();
            ioProvider.WriteLine(phraseProvider.GetPhrase("BlackList"));
            showClientsList(blackList);
            Logger.MainLog.Info("Successfully showed black list");
            return true;
        }

        private bool showFullClientsList(TradingDBContext db)
        {
            var fullList = db.Clients.ToList();
            ioProvider.WriteLine(phraseProvider.GetPhrase("FullList"));
            showClientsList(fullList);
            Logger.MainLog.Info("Successfully showed full client list");
            return true;
        }

        private bool showFullSharesList(TradingDBContext db)
        {
            var fullList = db.Shares.ToList();
            ioProvider.WriteLine(phraseProvider.GetPhrase("FullList"));
            ioProvider.WriteLine(phraseProvider.GetPhrase("ShareHeader"));
            foreach (Shares share in fullList)
            {
                string clientInfo = $"{share.ShareID.ToString()} {share.ShareName} {share.ShareCost.ToString()}";
                ioProvider.WriteLine(clientInfo);
            }
            Logger.MainLog.Info("Successfully showed full share list");
            return true;
        }

        private void showClientsList(List<Clients> clients)
        {
            ioProvider.WriteLine(phraseProvider.GetPhrase("ClientHeader"));
            foreach (Clients client in clients)
            {
                string clientInfo = $"{client.ClientID.ToString()} {client.ClientFirstName} {client.ClientLastName} {client.PhoneNumber} {client.ClientBalance.ToString()}";
                ioProvider.WriteLine(clientInfo);
            }
        }
    }
}
